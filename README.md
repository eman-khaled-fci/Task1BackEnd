# CSV Reader Backend

This backend project is part of a CSV Reader web application that allows users to upload CSV files, convert them to Excel format, and retrieve CSV data.

## Table of Contents
- [Demo](#demo)
- [Overview](#overview)
- [Endpoints](#endpoints)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)

## Demo

https://github.com/eman-khaled-fci/Task1BackEnd/assets/90774185/000a8f1b-7f08-4a0c-970b-aea501fc6bbb


## Overview

The CSV Reader Backend is developed using ASP.NET Core and provides RESTful APIs for handling CSV file operations. It includes functionality to upload CSV files, convert them to Excel format, and retrieve CSV data.

## Endpoints

- **Upload CSV File:**
  - `POST /api/csv/upload`
    - Uploads a CSV file.

- **Convert CSV to Excel:**
  - `GET /api/csv/convert-to-excel`
    - Converts the uploaded CSV file to Excel format.

- **Get CSV Data:**
  - `GET /api/csv/get-csv-data`
    - Retrieves CSV data from the uploaded file.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/eman-khaled-fci/Task1BackEnd.git
    ```

2. Navigate to the project directory:
    ```bash
    cd Task1BackEnd
    ```

3. Build and run the project:
    ```bash
    dotnet build
    dotnet run
    ```

## Configuration

- The backend uses a local folder named "uploads" to store uploaded CSV files.

## Usage

1. Send a POST request to `/api/csv/upload` to upload a CSV file.
2. Use the `/api/csv/convert-to-excel` endpoint to convert the uploaded CSV file to Excel format.
3. Retrieve CSV data using the `/api/csv/get-csv-data` endpoint.

