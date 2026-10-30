import UIKit

@UIApplicationMain
final class AppDelegate: UIResponder, UIApplicationDelegate {
    var window: UIWindow?

    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        window = UIWindow(frame: UIScreen.main.bounds)
        window?.rootViewController = ViewController()
        window?.makeKeyAndVisible()
        return true
    }
}

final class ViewController: UIViewController, UITableViewDataSource, UITableViewDelegate {
    private let store = SQLiteStore()
    private let tableView = UITableView()
    private let segmentControl = UISegmentedControl(items: ["Dashboard", "Index", "Notes"])
    private var themes: [Theme] = []
    private var markers: [IndexMarker] = []
    private var annotations: [Annotation] = []

    override func viewDidLoad() {
        super.viewDidLoad()
        view.backgroundColor = .white

        let titleLabel = UILabel()
        titleLabel.text = "Edwards Index AI"
        titleLabel.font = UIFont.boldSystemFont(ofSize: 28)
        titleLabel.translatesAutoresizingMaskIntoConstraints = false
        view.addSubview(titleLabel)

        segmentControl.selectedSegmentIndex = 0
        segmentControl.translatesAutoresizingMaskIntoConstraints = false
        view.addSubview(segmentControl)

        tableView.translatesAutoresizingMaskIntoConstraints = false
        tableView.dataSource = self
        tableView.delegate = self
        view.addSubview(tableView)

        NSLayoutConstraint.activate([
            titleLabel.topAnchor.constraint(equalTo: view.safeAreaLayoutGuide.topAnchor, constant: 20),
            titleLabel.leadingAnchor.constraint(equalTo: view.leadingAnchor, constant: 20),
            titleLabel.trailingAnchor.constraint(equalTo: view.trailingAnchor, constant: -20),

            segmentControl.topAnchor.constraint(equalTo: titleLabel.bottomAnchor, constant: 12),
            segmentControl.leadingAnchor.constraint(equalTo: view.leadingAnchor, constant: 20),
            segmentControl.trailingAnchor.constraint(equalTo: view.trailingAnchor, constant: -20),

            tableView.topAnchor.constraint(equalTo: segmentControl.bottomAnchor, constant: 16),
            tableView.leadingAnchor.constraint(equalTo: view.leadingAnchor),
            tableView.trailingAnchor.constraint(equalTo: view.trailingAnchor),
            tableView.bottomAnchor.constraint(equalTo: view.bottomAnchor)
        ])

        segmentControl.addTarget(self, action: #selector(segmentChanged), for: .valueChanged)

        loadData()
        segmentChanged()
    }

    @objc private func segmentChanged() {
        switch segmentControl.selectedSegmentIndex {
        case 0:
            renderDashboard()
        case 1:
            renderMarkers()
        case 2:
            renderAnnotations()
        default:
            renderDashboard()
        }
    }

    private func loadData() {
        themes = store.allThemes()
        markers = store.allMarkers()
        annotations = store.allAnnotations()
    }

    private func renderDashboard() {
        tableView.reloadData()
    }

    private func renderMarkers() {
        tableView.reloadData()
    }

    private func renderAnnotations() {
        tableView.reloadData()
    }

    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        switch segmentControl.selectedSegmentIndex {
        case 0:
            return themes.count > 0 ? themes.count : 1
        case 1:
            return markers.count > 0 ? markers.count : 1
        case 2:
            return annotations.count > 0 ? annotations.count : 1
        default:
            return 1
        }
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = UITableViewCell(style: .subtitle, reuseIdentifier: "Cell")

        switch segmentControl.selectedSegmentIndex {
        case 0:
            if themes.isEmpty {
                cell.textLabel?.text = "No themes available"
            } else {
                let theme = themes[indexPath.row]
                cell.textLabel?.text = "Theme: \(theme.name)"
                cell.detailTextLabel?.text = "Category: \(theme.category)"
            }
        case 1:
            if markers.isEmpty {
                cell.textLabel?.text = "No markers available"
            } else {
                let marker = markers[indexPath.row]
                cell.textLabel?.text = marker.alphanumericTag
                let coords = marker.coordinates.map { "\($0.bookName) \($0.chapter):\($0.verse)" }
                cell.detailTextLabel?.text = coords.joined(separator: ", ")
            }
        case 2:
            if annotations.isEmpty {
                cell.textLabel?.text = "No annotations available"
            } else {
                let annotation = annotations[indexPath.row]
                cell.textLabel?.text = annotation.text
                cell.detailTextLabel?.text = annotation.theme.name
            }
        default:
            cell.textLabel?.text = "Edwards Index AI"
        }

        return cell
    }
}
