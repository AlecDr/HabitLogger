
# Habit Tracker

## Overview
This .NET project is a console-based Habit Tracker application that allows users to manage their habits and track occurrences over time. The application is designed with a focus on clean architecture and separation of concerns.

## Project Structure
The project is organized into the following directories and files:

- **Program.cs**: The entry point of the application. This file contains the main method that initializes and runs the application.
  
- **Daos**:
  - `HabitsDao.cs`: Handles data access operations related to habits.
  - `HabitsOccurrencesDao.cs`: Manages data access operations related to the occurrences of habits.

- **Dtos**:
  - `HabitShowDTO.cs`: Data Transfer Object for displaying habit information.
  - `HabitStoreDTO.cs`: Data Transfer Object for storing habit information.
  - `HabitUpdateDTO.cs`: Data Transfer Object for updating habit information.
  - `HabitOccurrenceShowDTO.cs`: Data Transfer Object for displaying habit occurrence information.
  - `HabitOccurrenceStoreDTO.cs`: Data Transfer Object for storing habit occurrence information.

- **Helpers**:
  - `ConsoleHelper.cs`: Contains utility methods for interacting with the console.
  - `DatabaseHelper.cs`: Provides database-related utilities and connections.
  - `HabitsLoggerHelper.cs`: Handles logging operations within the application.

## Getting Started

### Prerequisites
- .NET SDK (version 5.0 or higher)

### Running the Application
1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Run the following command to build and run the application:
	 ```bash
   dotnet run
   ```
### Using the Application

Once the application is running, you can follow the console prompts to create, update, and track your habits.

## Future Enhancements

-   **UI Improvements**: Implement a graphical user interface (GUI) to replace the console-based interaction.
-   **Database Integration**: Expand the current database functionalities to support multiple users and more complex queries.
-   **Reporting**: Add features to generate reports on habit tracking over time.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License - see the LICENSE file for details.