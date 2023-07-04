# Country Controller API Documentation

## Application Description
The Country Controller API is a part of the Use Case 1 application and provides endpoints to retrieve information about countries. It utilizes the Microsoft ASP.NET Core framework to handle HTTP requests and responses. The main functionality of this API is to fetch country data from a REST API, apply various filters and sorting options, and return the filtered and sorted data to the client.

The API endpoint `/api/country` supports HTTP GET requests and accepts optional query parameters to filter, sort, and limit the number of countries returned. The available query parameters are as follows:
- `countryName`: Filters the countries by name. Only countries whose common name contains the specified value (case-insensitive) will be included in the result.
- `population`: Filters the countries by population. Only countries with a population less than the specified value (in millions) will be included in the result.
- `sort`: Sorts the countries based on the common name. The available sort options are `Ascending` and `Descending`.
- `firstCountries`: Limits the number of countries returned to the specified value. Only the first N countries (based on the sort order) will be included in the result.

The API makes use of an external REST API (`https://restcountries.com/v3.1/all`) to fetch the initial country data. It then applies the specified filters, sorting, and limit operations on this data to generate the final result.

## How to Run the Application Locally
To run the developed application locally, follow the steps below:

1. Set up the development environment:
    - Install the latest version of Visual Studio or Visual Studio Code.
    - Install the .NET Core SDK.

2. Clone the application repository:
   git clone <repository_url>

3. Navigate to the project directory:
   cd UseCase1

4. Build the application:
   dotnet build

5. Run the application:
   dotnet run

6. The application will start and listen for incoming HTTP requests on the specified port (usually `http://localhost:5000` or `https://localhost:5001`).

## Examples of Using the Endpoint
Here are some examples demonstrating how to use the developed endpoint:

1. Retrieve all countries:
   GET /api/country

2. Filter countries by name:
   GET /api/country?countryName=United

3. Filter countries by population:
   GET /api/country?population=100

4. Sort countries in ascending order:
   GET /api/country?sort=Ascending

5. Sort countries in descending order:
   GET /api/country?sort=Descending

6. Filter and sort countries:
   GET /api/country?countryName=United&sort=Descending

7. Limit the number of countries returned:
   GET /api/country?firstCountries=10

8. Filter, sort, and limit countries:
   GET /api/country?countryName=United&sort=Ascending&firstCountries=5

9. Filter by population, sort, and limit countries:
   GET /api/country?population=50&sort=Descending&firstCountries=3

10. Invalid request with missing query parameters:
 ```
 GET /api/country?sort=&firstCountries=
 ```

Note: Replace `http://localhost:5000` with the actual base URL of the application if it's running on a different port or domain.