# BankApp

BankApp is a cross-platform MAUI application designed to interact with the Test Bank API. It demonstrates a complete payment workflow using modern MVVM practices, Refit for API calls, custom animations, and UI effects.


## Project Structure

BankApp/
├── App.xaml
├── App.xaml.cs
├── MauiProgram.cs
├── BankApp.csproj
├── Models/
│   ├── Account.cs
│   ├── Beneficiary.cs
│   ├── PaymentInitialiseResponse.cs
│   ├── PaymentReviewRequest.cs
│   ├── PaymentReviewResponse.cs
│   ├── PaymentExecuteRequest.cs
│   └── PaymentExecuteResponse.cs
├── Services/
│   └── IBankApi.cs
├── ViewModels/
│   ├── BeneficiariesViewModel.cs
│   ├── PaymentFormViewModel.cs
│   ├── PaymentReviewViewModel.cs
│   └── ResultViewModel.cs
├── Views/
│   ├── BeneficiariesPage.xaml (+ .xaml.cs)
│   ├── PaymentFormPage.xaml (+ .xaml.cs)
│   ├── PaymentReviewPage.xaml (+ .xaml.cs)
│   └── ResultPage.xaml (+ .xaml.cs)
└── Behaviors/
    └── TapToBounceBehavior.cs


## Features

- **MVVM Architecture:**  
  - Clean separation between Views, ViewModels, and Models.
  - Uses commands and data-binding for interaction.

- **API Integration using Refit:**  
  - Communicates with the Test Bank API endpoints (`PaymentInitialise`, `PaymentReview`, and `PaymentExecute`).
  - Automatically includes a random 8-digit `UserKey` header for API authentication.

- **Beneficiaries List & Search:**  
  - Displays a list of beneficiaries retrieved from the API.
  - Includes a SearchBar to filter beneficiaries by name.
  - Shows a "No results found" message when the search yields no matches.

- **Payment Workflow:**  
  - **Payment Form Page:**  
    - Displays beneficiary details.
    - Lets users select an account and enter a payment amount.
    - Features a stylized numeric Entry with effects.
  - **Payment Review Page:**  
    - Shows a summary of the payment details (beneficiary, account, amount, fee).
    - Provides a "Make Payment" button that finalizes the transaction.
  - **Result Page:**  
    - Displays the payment reference after a successful payment.

- **Animations & Effects:**  
  - Fade-in animations on page transitions.
  - Bounce animations on tapped controls using both code‑behind and custom Behaviors.
  - Enhanced styling for inputs and controls.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [.NET MAUI workload](https://docs.microsoft.com/dotnet/maui/get-started/installation)
- An IDE that supports .NET MAUI (e.g., [Visual Studio 2022/2023](https://visualstudio.microsoft.com/))

## Building the Application

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/MeetRandy/BankApp-Practice-Assessment.git
   cd BankApp-Practice-Assessment

2. **Restore NuGet Packages:**

In your terminal or through Visual Studio, run:

    dotnet restore

3. **Build the Project:**

You can build the project from the command line:

    dotnet build

Or use Visual Studio's build functionality.
