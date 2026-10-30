import Foundation

@MainActor
final class AppViewModel: ObservableObject {
    @Published var themes: [Theme]
    @Published var markers: [IndexMarker]
    @Published var annotations: [Annotation]
    @Published var selectedTheme: Theme?

    init() {
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

        self.themes = themes
        self.selectedTheme = themes.first

        self.markers = [
            IndexMarker(alphanumericTag: "A001", dateCreated: Date().addingTimeInterval(-86400 * 12), theme: themes[0], coordinates: [coordinates[0]]),
            IndexMarker(alphanumericTag: "P204", dateCreated: Date().addingTimeInterval(-86400 * 8), theme: themes[1], coordinates: [coordinates[1]]),
            IndexMarker(alphanumericTag: "G118", dateCreated: Date().addingTimeInterval(-86400 * 20), theme: themes[2], coordinates: [coordinates[2]]),
            IndexMarker(alphanumericTag: "H009", dateCreated: Date().addingTimeInterval(-86400 * 4), theme: themes[3], coordinates: [coordinates[3]])
        ]

        self.annotations = [
            Annotation(text: "The theme of providence appears to connect with the believer’s endurance through trials and uncertainty.", dateCreated: Date().addingTimeInterval(-86400 * 5), theme: themes[0]),
            Annotation(text: "Prayer is not merely a request but a posture of dependence and peace in anxious seasons.", dateCreated: Date().addingTimeInterval(-86400 * 11), theme: themes[1]),
            Annotation(text: "Grace is the divine gift that transforms a person before they can merit it through effort.", dateCreated: Date().addingTimeInterval(-86400 * 18), theme: themes[2]),
            Annotation(text: "Humility is the prerequisite for receiving the Lord’s guidance and restoration.", dateCreated: Date().addingTimeInterval(-86400 * 2), theme: themes[3])
        ]
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
