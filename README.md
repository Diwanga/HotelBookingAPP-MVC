# 🏨 HotelBookingAPP

A simple ASP.NET Core MVC application for managing hotel bookings, generating reports, and predicting future room availability using basic pattern analysis.

---

## 📚 Overview

HotelBookingAPP is a lightweight, educational project designed to:
- Record hotel room bookings and special requests
- Generate weekly booking reports
- Predict future room availability and pricing using a chatbot
- Demonstrate core MVC concepts using .NET 8 and Bootstrap

---

## 🛠️ Technologies Used

- **ASP.NET Core MVC (.NET 8)**
- **C#**
- **Bootstrap 5**
- **In-memory data (no database required)**

---

## 🚀 Features

- ✅ Add, edit, and delete hotel bookings
- ✅ Associate bookings with rooms and guest names
- ✅ Prevent overlapping bookings with validation
- ✅ Manage rooms (type, capacity, price per day)
- ✅ Automatically calculate total booking price
- ✅ Add special requests to bookings
- ✅ Generate a weekly report of bookings by day
- ✅ Ask a chatbot to:
  - Predict room availability based on last month’s usage
  - Estimate average room price for given dates

---

## 📂 Project Structure

<img width="441" height="433" alt="image" src="https://github.com/user-attachments/assets/acf8137c-40d6-469a-bb67-df12373750ab" />


---

## 🧪 Sample Data

The system is preloaded with:
- **10 Rooms** (Deluxe and Standard)
- **100 Bookings** from the previous month
- All bookings include dates, room types, and special requests

---

## 💬 Chatbot Query Examples

| Intent | Example Query |
|--------|----------------|
| Predict with pricing | `Will it be available to get 3 rooms in 2025/08/06 to 2025/08/07?` |

---

## ▶️ How to Run

1. **Install .NET 8 SDK**  
   https://dotnet.microsoft.com/download

2. **Clone this repository**

```bash
git clone <this repo url>
cd HotelBookingAPP-MVC
```
3. Run the app

```bash
dotnet run
```

4. Open in browser
Navigate to http://localhost:xxxx (the port will be shown in the terminal)

APP Screenshots : 

<img width="1411" height="616" alt="image" src="https://github.com/user-attachments/assets/d3e9106b-704a-4075-ad06-7c028fa361d5" />
 
Report for More Detail:  [Cw1_W2151429.pdf](https://github.com/user-attachments/files/21330719/Cw1_W2151429.pdf)


