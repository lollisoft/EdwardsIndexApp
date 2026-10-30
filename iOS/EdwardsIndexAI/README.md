// swift-tools-version: 5.9
import PackageDescription

let package = Package(
    name: "EdwardsIndexAI",
    platforms: [
        .iOS(.v17)
    ],
    products: [
        .library(
            name: "EdwardsIndexAI",
            targets: ["EdwardsIndexAI"]
        )
    ],
    targets: [
        .target(
            name: "EdwardsIndexAI",
            path: "Sources/EdwardsIndexAI",
            resources: [
                .copy("Resources/EdwardsIndexSchema.txt")
            ]
        )
    ]
)
