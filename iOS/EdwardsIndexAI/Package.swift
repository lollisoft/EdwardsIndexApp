import SwiftUI

@main
struct EdwardsIndexAIApp: App {
    var body: some Scene {
        WindowGroup {
            ContentView()
        }
    }
}

struct ContentView: View {
    @StateObject private var viewModel = AppViewModel()

    var body: some View {
        TabView {
            DashboardView(viewModel: viewModel)
                .tabItem {
                    Label("Dashboard", systemImage: "sparkles")
                }

            IndexView(viewModel: viewModel)
                .tabItem {
                    Label("Index", systemImage: "list.bullet")
                }

            NotesView(viewModel: viewModel)
                .tabItem {
                    Label("Notes", systemImage: "doc.text")
                }
        }
        .tint(.indigo)
    }
}

struct DashboardView: View {
    @ObservedObject var viewModel: AppViewModel

    var body: some View {
        NavigationStack {
            ScrollView {
                VStack(alignment: .leading, spacing: 20) {
                    HeaderCard(
                        title: "Edwards Index AI",
                        subtitle: "A SwiftUI showcase that maps the UML model into a modern iOS study experience using local SQLite persistence."
                    )

                    Picker("Theme", selection: Binding(
                        get: { viewModel.selectedTheme ?? viewModel.themes.first! },
                        set: { viewModel.selectedTheme = $0; viewModel.persist() }
                    )) {
                        ForEach(viewModel.themes) { theme in
                            Text(theme.name).tag(theme)
                        }
                    }
                    .pickerStyle(.segmented)

                    AIInsightCard(summary: viewModel.aiSummary())

                    HStack(spacing: 16) {
                        StatCard(title: "Markers", value: "\(viewModel.selectedThemeMarkers.count)")
                        StatCard(title: "Notes", value: "\(viewModel.selectedThemeAnnotations.count)")
                        StatCard(title: "Themes", value: "\(viewModel.themes.count)")
                    }

                    VStack(alignment: .leading, spacing: 12) {
                        Text("AI insights")
                            .font(.headline)

                        ForEach(viewModel.generateInsights()) { insight in
                            InsightRow(insight: insight)
                        }
                    }
                }
                .padding()
            }
            .navigationTitle("Dashboard")
        }
    }
}

struct HeaderCard: View {
    let title: String
    let subtitle: String

    var body: some View {
        VStack(alignment: .leading, spacing: 8) {
            Text(title)
                .font(.largeTitle.bold())
            Text(subtitle)
                .font(.subheadline)
                .foregroundStyle(.secondary)
        }
        .padding()
        .frame(maxWidth: .infinity, alignment: .leading)
        .background(
            LinearGradient(colors: [.indigo.opacity(0.9), .purple.opacity(0.7)], startPoint: .topLeading, endPoint: .bottomTrailing)
        )
        .clipShape(RoundedRectangle(cornerRadius: 20, style: .continuous))
    }
}

struct AIInsightCard: View {
    let summary: String

    var body: some View {
        VStack(alignment: .leading, spacing: 12) {
            Label("AI summary", systemImage: "brain.head.profile")
                .font(.headline)
            Text(summary)
                .font(.body)
                .foregroundStyle(.primary)
        }
        .padding()
        .frame(maxWidth: .infinity, alignment: .leading)
        .background(Color(.secondarySystemBackground))
        .clipShape(RoundedRectangle(cornerRadius: 18, style: .continuous))
    }
}

struct StatCard: View {
    let title: String
    let value: String

    var body: some View {
        VStack(spacing: 8) {
            Text(value)
                .font(.title2.bold())
            Text(title)
                .font(.caption)
                .foregroundStyle(.secondary)
        }
        .frame(maxWidth: .infinity)
        .padding()
        .background(Color(.secondarySystemBackground))
        .clipShape(RoundedRectangle(cornerRadius: 16, style: .continuous))
    }
}

struct InsightRow: View {
    let insight: AIInsight

    var body: some View {
        VStack(alignment: .leading, spacing: 6) {
            HStack {
                Text(insight.title)
                    .font(.headline)
                Spacer()
                Text("\(insight.confidence)%")
                    .font(.caption.bold())
                    .padding(6)
                    .background(Color.indigo.opacity(0.12))
                    .clipShape(Capsule())
            }
            Text(insight.summary)
                .font(.subheadline)
                .foregroundStyle(.secondary)
        }
        .padding()
        .frame(maxWidth: .infinity, alignment: .leading)
        .background(Color(.secondarySystemBackground))
        .clipShape(RoundedRectangle(cornerRadius: 16, style: .continuous))
    }
}

struct IndexView: View {
    @ObservedObject var viewModel: AppViewModel

    var body: some View {
        NavigationStack {
            List(viewModel.selectedThemeMarkers) { marker in
                VStack(alignment: .leading, spacing: 8) {
                    Text(marker.alphanumericTag)
                        .font(.title3.bold())
                    Text("Created: \(marker.dateCreated.formatted(date: .abbreviated, time: .omitted))")
                        .font(.caption)
                        .foregroundStyle(.secondary)

                    ForEach(marker.coordinates) { coordinate in
                        Text("\(coordinate.bookName) \(coordinate.chapter):\(coordinate.verse) • Page \(coordinate.page)")
                            .font(.subheadline)
                    }
                }
                .padding(.vertical, 4)
            }
            .navigationTitle("Index markers")
        }
    }
}

struct NotesView: View {
    @ObservedObject var viewModel: AppViewModel

    var body: some View {
        NavigationStack {
            List(viewModel.selectedThemeAnnotations) { annotation in
                VStack(alignment: .leading, spacing: 8) {
                    Text(annotation.text)
                        .font(.body)
                    Text(annotation.dateCreated.formatted(date: .abbreviated, time: .shortened))
                        .font(.caption)
                        .foregroundStyle(.secondary)
                }
                .padding(.vertical, 4)
            }
            .navigationTitle("Notes")
        }
    }
}

#Preview {
    ContentView()
}
