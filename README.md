# DLand — Character Manager

> Менеджер персонажей для фэнтези-RPG · Fantasy RPG character manager

---

## Русский

**DLand** — это десктопное WPF-приложение для управления персонажами в стиле фэнтези-ролевых игр. Приложение позволяет пользователям создавать, просматривать, редактировать и искать героев с полным набором характеристик, способностей и инвентаря.

### ✨ Возможности

- 🔐 **Авторизация пользователей** — вход по логину и паролю с разграничением прав доступа
- 🦸 **Управление героями** — создание, редактирование и просмотр персонажей
- 📊 **Характеристики персонажа** — Сила, Магия, Выносливость, Ловкость, Мудрость, Удача, Вес, Стресс, Скорость, HP, MP
- ✨ **Способности** — активные и пассивные умения персонажа
- 🎒 **Инвентарь** — предметы с описанием и весом
- 🖼️ **Изображения** — загрузка и отображение портретов героев
- 🔍 **Поиск** — быстрый поиск героев по имени с задержкой ввода (debounce)
- 🎨 **Современный UI** — Material Design интерфейс на WPF

### 🏗️ Архитектура

Проект построен на **WPF (.NET)** с использованием следующих технологий:

| Технология | Назначение |
|---|---|
| **WPF** | Графический интерфейс |
| **Dapper** | Микро-ORM для работы с БД |
| **Npgsql** | Драйвер PostgreSQL |
| **MaterialDesignThemes** | UI-компоненты |
| **XamlReader** | Динамическая загрузка карточек из XAML |

#### Структура проекта

```text
DLand/
├── backend/models/        # Модели данных (Hero, User)
├── Model/                 # Дополнительные модели (Properties, Abilities, Items)
├── Utilities/             # Логика приложения
│   ├── Database.cs        # Работа с PostgreSQL через Dapper
│   ├── Load.cs            # Загрузка данных и карточек героев
│   ├── HeroCard.cs        # Отображение карточки персонажа
│   ├── Log_in_Out.cs      # Авторизация
│   └── Convertation.cs    # Конвертация изображений
├── ViewModel/             # XAML-шаблоны карточек
│   ├── HeroCard.xaml
│   ├── SpellCard.xaml
│   └── InventoryCard.xaml
├── MainWindow.xaml        # Главное окно
└── CharacterWindow.xaml   # Окно персонажа
```

### 🗄️ База данных

Приложение использует **PostgreSQL**. Основные таблицы:

- `DL_USERS` — пользователи
- `DL_HEROES` — герои
- `DL_HEROESPROPERTIES` — характеристики героев
- `DL_HEROESABILITIES` — способности
- `DL_HEROESITEMS` — инвентарь

### 🚀 Запуск

1. Клонируйте репозиторий:

2. Установите PostgreSQL и создайте базу данных.

3. Настройте строку подключения в `Database.cs`:

   ```csharp
   private string _connectionString =
       "Host=...;Port=5432;Database=...;Username=...;Password=...";
   ```

4. Откройте решение в **Visual Studio** и запустите проект.

### 👤 Права доступа

- Обычные пользователи могут редактировать только **своих** героев.
- Администратор (логин `galdnext`) имеет доступ к редактированию всех персонажей.

---

## English

**DLand** is a desktop WPF application for managing fantasy RPG-style characters. It allows users to create, view, edit, and search heroes with a full set of stats, abilities, and inventory.

### ✨ Features

- 🔐 **User authentication** — login/password with role-based access control
- 🦸 **Hero management** — create, edit, and view characters
- 📊 **Character stats** — Power, Magic, Durability, Dexterity, Wisdom, Luck, Weight, Stress, Speed, HP, MP
- ✨ **Abilities** — active and passive skills
- 🎒 **Inventory** — items with description and weight
- 🖼️ **Images** — upload and display hero portraits
- 🔍 **Search** — fast hero search by name with input debounce
- 🎨 **Modern UI** — Material Design interface built on WPF

### 🏗️ Architecture

The project is built on **WPF (.NET)** using the following technologies:

| Technology | Purpose |
|---|---|
| **WPF** | User interface |
| **Dapper** | Micro-ORM for database access |
| **Npgsql** | PostgreSQL driver |
| **MaterialDesignThemes** | UI components |
| **XamlReader** | Dynamic card loading from XAML |

#### Project Structure

```text
DLand/
├── backend/models/        # Data models (Hero, User)
├── Model/                 # Additional models (Properties, Abilities, Items)
├── Utilities/             # Application logic
│   ├── Database.cs        # PostgreSQL access via Dapper
│   ├── Load.cs            # Data & hero card loading
│   ├── HeroCard.cs        # Character card rendering
│   ├── Log_in_Out.cs      # Authentication
│   └── Convertation.cs    # Image conversion
├── ViewModel/             # XAML card templates
│   ├── HeroCard.xaml
│   ├── SpellCard.xaml
│   └── InventoryCard.xaml
├── MainWindow.xaml        # Main window
└── CharacterWindow.xaml   # Character window
```

### 🗄️ Database

The app uses **PostgreSQL**. Main tables:

- `DL_USERS` — users
- `DL_HEROES` — heroes
- `DL_HEROESPROPERTIES` — hero stats
- `DL_HEROESABILITIES` — abilities
- `DL_HEROESITEMS` — inventory

### 🚀 Getting Started

1. Clone the repository:

2. Install PostgreSQL and create a database.

3. Configure the connection string in `Database.cs`:

   ```csharp
   private string _connectionString =
       "Host=...;Port=5432;Database=...;Username=...;Password=...";
   ```

4. Open the solution in **Visual Studio** and run the project.

### 👤 Access Rights

- Regular users can only edit **their own** heroes.
- The admin (login `galdnext`) has access to edit all characters.

---
