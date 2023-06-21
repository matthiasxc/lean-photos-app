# lean-photos-app
A technical showcase for Lean Techniques demonstrating a photo album app created by Matthias Shapiro

## Application Langauge and Framework
This is a UWP application built in Visual Studio 2022 using C# and XAML and can be run on Windows or XBox. It employs MVVM (Model-View-ViewModel) architecture which allows us a fairly clean seperation of concerns between the user interface and the core application logic.

## Application Structure
This app is made of 3 projects

- LeanPhotos.App - This is the executable application and contains the application framework and runtime, the UI, image assets, and package manifest. The XAML-based UI is databound to the controlling MainPageViewModel for all application states and underlying data. It features
-- virtualized GridView control for viewing very large sets of photo data
-- a simple master-detail view, allowing users to select photos to see them in more detail and examine the photo metadata
-- a text input box for inputing query data that will retrieve specific photos in certain albums of the application
-- UI components to indicate the application state to the user (error states, album view vs all photos, selected vs unselected views) 

- LeanPhotos.Logic - This project contains the core business logic for the application
-- PhotoService - for retrieving the photo data and converting it from JSON to C# objects
-- InputValidation - a static helper for validating query input
-- MainPageViewModel - the engine of the app  utilizing the services and helpers to provide the underlying data and manage the application state

- LeanPhotos.Tests - A testing library. This tests the service data retrieval, the input validation helper, and the ViewModel to verify the commands that the data are appropriately passed and the application is in the appropriate state given both valid and invalid commands and user input.

## 3rd Party Libraries

This application includes the following 3rd party packages:

- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) - This toolkit provides many helpful MVVM tools including ObservableObject, AsyncRelayCommand, and an IoC (Inversion of Control) helper to help provide a clean structure for the application architecture

- [Microsoft.Toolkit.Uwp.UI](https://github.com/CommunityToolkit/WindowsCommunityToolkit) - Provides the BoolToVisibilityConverter for binding visibility states to boolean ViewModel properties

- [Newtonsoft.Json](https://www.newtonsoft.com/json) - This popular .NET JSON framwork provides helpers for error-tolerant JSON serialization and deserialization

## In Conclusion

Thank you for your consideration and I look forward to discussing this project further.