# Klazapp Enum Generator for Unity

## Introduction
The Klazapp Enum Generator is an advanced Unity Editor tool designed to facilitate the swift creation of enumeration (enum) types within the Unity environment. By automating the enum generation process, it significantly enhances productivity, allowing developers to focus more on game development rather than routine coding tasks.

## Features
- **User-Friendly Interface:** Utilizes a simple graphical user interface within the Unity Editor for easy interaction.
- **Customizable Enum Values:** Enables the dynamic addition, modification, and removal of enum values directly within the editor.
- **Automatic Value Assignment:** Supports both automatic and manual assignment of integer values to enum entries.
- **Icon Integration:** Enhances user experience with intuitive icons, making the tool more accessible and easier to use.

## Dependencies
- **Unity Version:** Unity 2020.3 LTS or newer is required to ensure full compatibility and performance.
- **Unity Editor:** This tool is designed to operate within the Unity Editor and is not applicable to runtime game scripts.

## Compatibility
The Enum Generator tool is compatible with all major Unity rendering pipelines:
| Compatibility | URP | BRP | HDRP |
|---------------|-----|-----|------|
| Compatible    | ✔️   | ✔️   | ✔️    |

## Installation
1. Download or clone the repository containing the Enum Generator package from [Klazapp Enum Generator Repository](https://github.com/klazapp/Unity-Enum-Generator-Public.git).
2. Import the package into your Unity project, ensuring it resides under the `Assets` or `Packages` directory.
3. Access the tool via the Unity Editor menu to begin generating enums.

## Usage
To generate enums using the Klazapp Enum Generator:
1. Open the Enum Generator tool by navigating to `Klazapp > Tools > Enum Generator` in the Unity Editor's menu bar.
2. In the GUI, specify the desired name for your enum in the 'Enum Name' field.
3. Add or modify enum entries as needed. Each entry can have an integer value assigned or left to automatic assignment.
4. Click the 'Generate' button to create and save the enum script in a chosen location within your project.
5. The generated enum will be compiled by Unity, allowing it to be used immediately within your scripts.

## To-Do List (Future Features)
- Enhance error handling to provide feedback directly in the editor for invalid inputs.
- Implement ordering and formatting options for enum values.
- Support for attributes and comments within the enum to facilitate better code documentation.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
