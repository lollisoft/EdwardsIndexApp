import Foundation
import SQLite3

final class LocalSQLiteStore {
    static let databaseName = "EdwardsIndexAI.sqlite"

    private let databaseURL: URL
    private let schemaText: String
    private var db: OpaquePointer?

    init() {
        let fileManager = FileManager.default
        let documentsURL = fileManager.urls(for: .documentDirectory, in: .userDomainMask).first ?? fileManager.temporaryDirectory
        self.databaseURL = documentsURL.appendingPathComponent(Self.databaseName)

        if let url = Bundle.module.url(forResource: "EdwardsIndexSchema", withExtension: "txt") {
            self.schemaText = (try? String(contentsOf: url, encoding: .utf8)) ?? Self.defaultSchema
        } else {
            self.schemaText = Self.defaultSchema
        }

        self.db = nil
        setupDatabase()
    }

    static var defaultSchema: String {
        """
        <schema name="EdwardsIndexAI">
          <entity name="Theme">
            <field name="id" type="String"/>
            <field name="name" type="String"/>
            <field name="category" type="String"/>
          </entity>
          <entity name="IndexMarker">
            <field name="id" type="String"/>
            <field name="alphanumericTag" type="String"/>
            <field name="dateCreated" type="Date"/>
            <field name="themeId" type="String"/>
          </entity>
          <entity name="BookCoordinate">
            <field name="id" type="String"/>
            <field name="markerId" type="String"/>
            <field name="bookName" type="String"/>
            <field name="chapter" type="Int"/>
            <field name="verse" type="Int"/>
            <field name="page" type="Int"/>
            <field name="subject" type="String"/>
          </entity>
          <entity name="Annotation">
            <field name="id" type="String"/>
            <field name="text" type="String"/>
            <field name="dateCreated" type="Date"/>
            <field name="themeId" type="String"/>
          </entity>
        </schema>
        """
    }

    private func setupDatabase() {
        guard sqlite3_open_v2(databaseURL.path, &db, SQLITE_OPEN_READWRITE | SQLITE_OPEN_CREATE, nil) == SQLITE_OK else {
            print("Failed to open SQLite database at \(databaseURL.path)")
            return
        }

        let createTheme = """
            CREATE TABLE IF NOT EXISTS Theme (
                id TEXT PRIMARY KEY,
                name TEXT NOT NULL,
                category TEXT NOT NULL
            );
        """

        let createIndexMarker = """
            CREATE TABLE IF NOT EXISTS IndexMarker (
                id TEXT PRIMARY KEY,
                alphanumericTag TEXT NOT NULL,
                dateCreated TEXT NOT NULL,
                themeId TEXT NOT NULL,
                FOREIGN KEY(themeId) REFERENCES Theme(id)
            );
        """

        let createBookCoordinate = """
            CREATE TABLE IF NOT EXISTS BookCoordinate (
                id TEXT PRIMARY KEY,
                markerId TEXT NOT NULL,
                bookName TEXT NOT NULL,
                chapter INTEGER NOT NULL,
                verse INTEGER NOT NULL,
                page INTEGER NOT NULL,
                subject TEXT NOT NULL,
                FOREIGN KEY(markerId) REFERENCES IndexMarker(id)
            );
        """

        let createAnnotation = """
            CREATE TABLE IF NOT EXISTS Annotation (
                id TEXT PRIMARY KEY,
                text TEXT NOT NULL,
                dateCreated TEXT NOT NULL,
                themeId TEXT NOT NULL,
                FOREIGN KEY(themeId) REFERENCES Theme(id)
            );
        """

        execute(createTheme)
        execute(createIndexMarker)
        execute(createBookCoordinate)
        execute(createAnnotation)

        if countRows(in: "Theme") == 0 {
            let seed = makeSeedSnapshot()
            try? save(seed)
        }
    }

    private func execute(_ sql: String) {
        var statement: OpaquePointer?
        guard sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK else {
            print("Failed to prepare SQL: \(sql)")
            return
        }

        if sqlite3_step(statement) != SQLITE_DONE {
            print("Failed to execute SQL: \(sql)")
        }

        sqlite3_finalize(statement)
    }

    private func countRows(in table: String) -> Int {
        let sql = "SELECT COUNT(*) FROM \(table);"
        var statement: OpaquePointer?
        guard sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK else {
            return 0
        }

        var count = 0
        if sqlite3_step(statement) == SQLITE_ROW {
            count = Int(sqlite3_column_int(statement, 0))
        }

        sqlite3_finalize(statement)
        return count
    }

    func makeSeedSnapshot() -> EdwardsIndexSnapshot {
        let themes = [
            Theme(name: "Providence", category: "Doctrine"),
            Theme(name: "Prayer", category: "Devotion"),
            Theme(name: "Grace", category: "Theology"),
            Theme(name: "Humility", category: "Character")
        ]

        let coordinates = [
            BookCoordinate(bookName: "Romans", chapter: 8, verse: 28, page: 142, subject: "All things work together"),
            BookCoordinate(bookName: "Philippians", chapter: 4, verse: 6, page: 201, subject: "Prayer and peace"),
            BookCoordinate(bookName: "Ephesians", chapter: 2, verse: 8, page: 177, subject: "Grace through faith"),
            BookCoordinate(bookName: "James", chapter: 4, verse: 10, page: 88, subject: "Humility before God")
        ]

        let markers = [
            IndexMarker(alphanumericTag: "A001", dateCreated: Date().addingTimeInterval(-86400 * 12), theme: themes[0], coordinates: [coordinates[0]]),
            IndexMarker(alphanumericTag: "P204", dateCreated: Date().addingTimeInterval(-86400 * 8), theme: themes[1], coordinates: [coordinates[1]]),
            IndexMarker(alphanumericTag: "G118", dateCreated: Date().addingTimeInterval(-86400 * 20), theme: themes[2], coordinates: [coordinates[2]]),
            IndexMarker(alphanumericTag: "H009", dateCreated: Date().addingTimeInterval(-86400 * 4), theme: themes[3], coordinates: [coordinates[3]])
        ]

        let annotations = [
            Annotation(text: "The theme of providence appears to connect with the believer’s endurance through trials and uncertainty.", dateCreated: Date().addingTimeInterval(-86400 * 5), theme: themes[0]),
            Annotation(text: "Prayer is not merely a request but a posture of dependence and peace in anxious seasons.", dateCreated: Date().addingTimeInterval(-86400 * 11), theme: themes[1]),
            Annotation(text: "Grace is the divine gift that transforms a person before they can merit it through effort.", dateCreated: Date().addingTimeInterval(-86400 * 18), theme: themes[2]),
            Annotation(text: "Humility is the prerequisite for receiving the Lord’s guidance and restoration.", dateCreated: Date().addingTimeInterval(-86400 * 2), theme: themes[3])
        ]

        return EdwardsIndexSnapshot(themes: themes, markers: markers, annotations: annotations)
    }

    func loadSnapshot() throws -> EdwardsIndexSnapshot {
        let themes = try loadThemes()
        let markers = try loadMarkers(themes: themes)
        let annotations = try loadAnnotations(themes: themes)
        return EdwardsIndexSnapshot(themes: themes, markers: markers, annotations: annotations)
    }

    private func loadThemes() throws -> [Theme] {
        let sql = "SELECT id, name, category FROM Theme ORDER BY name;"
        return try executeQuery(sql) { statement in
            let id = String(cString: sqlite3_column_text(statement, 0))
            let name = String(cString: sqlite3_column_text(statement, 1))
            let category = String(cString: sqlite3_column_text(statement, 2))
            return Theme(id: UUID(uuidString: id) ?? UUID(), name: name, category: category)
        }
    }

    private func loadMarkers(themes: [Theme]) throws -> [IndexMarker] {
        let sql = "SELECT id, alphanumericTag, dateCreated, themeId FROM IndexMarker ORDER BY dateCreated DESC;"
        let rows = try executeQuery(sql) { statement in
            let id = String(cString: sqlite3_column_text(statement, 0))
            let tag = String(cString: sqlite3_column_text(statement, 1))
            let dateText = String(cString: sqlite3_column_text(statement, 2))
            let themeId = String(cString: sqlite3_column_text(statement, 3))
            let date = ISO8601DateFormatter().date(from: dateText) ?? Date()
            let theme = themes.first { $0.id.uuidString == themeId } ?? themes[0]
            let coordinates = try self.loadCoordinates(for: UUID(uuidString: id) ?? UUID())
            return IndexMarker(id: UUID(uuidString: id) ?? UUID(), alphanumericTag: tag, dateCreated: date, theme: theme, coordinates: coordinates)
        }
        return rows
    }

    private func loadCoordinates(for markerID: UUID) throws -> [BookCoordinate] {
        let sql = "SELECT id, bookName, chapter, verse, page, subject FROM BookCoordinate WHERE markerId = '\(markerID.uuidString)';"
        return try executeQuery(sql) { statement in
            let id = String(cString: sqlite3_column_text(statement, 0))
            let bookName = String(cString: sqlite3_column_text(statement, 1))
            let chapter = Int(sqlite3_column_int(statement, 2))
            let verse = Int(sqlite3_column_int(statement, 3))
            let page = Int(sqlite3_column_int(statement, 4))
            let subject = String(cString: sqlite3_column_text(statement, 5))
            return BookCoordinate(id: UUID(uuidString: id) ?? UUID(), bookName: bookName, chapter: chapter, verse: verse, page: page, subject: subject)
        }
    }

    private func loadAnnotations(themes: [Theme]) throws -> [Annotation] {
        let sql = "SELECT id, text, dateCreated, themeId FROM Annotation ORDER BY dateCreated DESC;"
        return try executeQuery(sql) { statement in
            let id = String(cString: sqlite3_column_text(statement, 0))
            let text = String(cString: sqlite3_column_text(statement, 1))
            let dateText = String(cString: sqlite3_column_text(statement, 2))
            let themeId = String(cString: sqlite3_column_text(statement, 3))
            let date = ISO8601DateFormatter().date(from: dateText) ?? Date()
            let theme = themes.first { $0.id.uuidString == themeId } ?? themes[0]
            return Annotation(id: UUID(uuidString: id) ?? UUID(), text: text, dateCreated: date, theme: theme)
        }
    }

    private func executeQuery<T>(_ sql: String, mapRow: (OpaquePointer) throws -> T) throws -> [T] {
        var statement: OpaquePointer?
        guard sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK else {
            throw SQLiteError.prepareFailed
        }

        defer { sqlite3_finalize(statement) }

        var results: [T] = []
        while sqlite3_step(statement) == SQLITE_ROW {
            results.append(try mapRow(statement))
        }
        return results
    }

    func save(_ snapshot: EdwardsIndexSnapshot) throws {
        clearAllTables()

        for theme in snapshot.themes {
            try insertTheme(theme)
        }

        for marker in snapshot.markers {
            try insertMarker(marker)
            for coordinate in marker.coordinates {
                try insertCoordinate(markerID: marker.id, coordinate: coordinate)
            }
        }

        for annotation in snapshot.annotations {
            try insertAnnotation(annotation)
        }
    }

    private func clearAllTables() {
        execute("DELETE FROM BookCoordinate;")
        execute("DELETE FROM Annotation;")
        execute("DELETE FROM IndexMarker;")
        execute("DELETE FROM Theme;")
    }

    private func insertTheme(_ theme: Theme) throws {
        let sql = "INSERT INTO Theme (id, name, category) VALUES (?, ?, ?);"
        try executeInsert(sql, values: [theme.id.uuidString, theme.name, theme.category])
    }

    private func insertMarker(_ marker: IndexMarker) throws {
        let sql = "INSERT INTO IndexMarker (id, alphanumericTag, dateCreated, themeId) VALUES (?, ?, ?, ?);"
        let dateValue = ISO8601DateFormatter().string(from: marker.dateCreated)
        try executeInsert(sql, values: [marker.id.uuidString, marker.alphanumericTag, dateValue, marker.theme.id.uuidString])
    }

    private func insertCoordinate(markerID: UUID, coordinate: BookCoordinate) throws {
        let sql = "INSERT INTO BookCoordinate (id, markerId, bookName, chapter, verse, page, subject) VALUES (?, ?, ?, ?, ?, ?, ?);"
        try executeInsert(sql, values: [coordinate.id.uuidString, markerID.uuidString, coordinate.bookName, coordinate.chapter, coordinate.verse, coordinate.page, coordinate.subject])
    }

    private func insertAnnotation(_ annotation: Annotation) throws {
        let sql = "INSERT INTO Annotation (id, text, dateCreated, themeId) VALUES (?, ?, ?, ?);"
        let dateValue = ISO8601DateFormatter().string(from: annotation.dateCreated)
        try executeInsert(sql, values: [annotation.id.uuidString, annotation.text, dateValue, annotation.theme.id.uuidString])
    }

    private func executeInsert(_ sql: String, values: [Any]) throws {
        var statement: OpaquePointer?
        guard sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK else {
            throw SQLiteError.prepareFailed
        }

        defer { sqlite3_finalize(statement) }

        for (index, value) in values.enumerated() {
            let sqliteIndex = Int32(index + 1)
            switch value {
            case let string as String:
                sqlite3_bind_text(statement, sqliteIndex, (string as NSString).utf8String, -1, nil)
            case let int as Int:
                sqlite3_bind_int(statement, sqliteIndex, Int32(int))
            default:
                sqlite3_bind_text(statement, sqliteIndex, "", -1, nil)
            }
        }

        guard sqlite3_step(statement) == SQLITE_DONE else {
            throw SQLiteError.executionFailed
        }
    }
}

enum SQLiteError: Error {
    case prepareFailed
    case executionFailed
}

@MainActor
final class AppViewModel: ObservableObject {
    @Published var themes: [Theme]
    @Published var markers: [IndexMarker]
    @Published var annotations: [Annotation]
    @Published var selectedTheme: Theme?

    private let store: LocalSQLiteStore

    init(store: LocalSQLiteStore = LocalSQLiteStore()) {
        self.store = store

        do {
            let snapshot = try store.loadSnapshot()
            self.themes = snapshot.themes
            self.markers = snapshot.markers
            self.annotations = snapshot.annotations
            self.selectedTheme = snapshot.themes.first
        } catch {
            let seed = store.makeSeedSnapshot()
            self.themes = seed.themes
            self.markers = seed.markers
            self.annotations = seed.annotations
            self.selectedTheme = seed.themes.first
            try? store.save(seed)
        }
    }

    func persist() {
        let snapshot = EdwardsIndexSnapshot(themes: themes, markers: markers, annotations: annotations)
        do {
            try store.save(snapshot)
        } catch {
            print("Persistence error: \(error)")
        }
    }

    var selectedThemeMarkers: [IndexMarker] {
        guard let selectedTheme else { return markers }
        return markers.filter { $0.theme.id == selectedTheme.id }
    }

    var selectedThemeAnnotations: [Annotation] {
        guard let selectedTheme else { return annotations }
        return annotations.filter { $0.theme.id == selectedTheme.id }
    }

    func aiSummary() -> String {
        guard let selectedTheme else {
            return "No theme selected. Choose a concept to reveal AI-driven insights."
        }

        let markerCount = selectedThemeMarkers.count
        let annotationCount = selectedThemeAnnotations.count
        let coordinates = selectedThemeMarkers.flatMap { $0.coordinates }

        return "AI insight for \(selectedTheme.name): this theme appears in \(markerCount) index markers and \(annotationCount) note entries. Cross-reference data points to \(coordinates.count) verse locations, suggesting a strong doctrinal thread around \(selectedTheme.category.lowercased())."
    }

    func generateInsights() -> [AIInsight] {
        let themeSummary = aiSummary()
        return [
            AIInsight(title: "Pattern Match", summary: themeSummary, confidence: 92),
            AIInsight(title: "Recommended Next Step", summary: "Review the related manuscript references and capture follow-up questions for sermon or study application.", confidence: 89),
            AIInsight(title: "Cross-Reference Suggestion", summary: "Compare this concept with adjacent themes to deepen the theological connection and map the narrative arc.", confidence: 86)
        ]
    }
}
