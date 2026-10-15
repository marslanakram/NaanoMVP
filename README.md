# Naano - Creator Marketplace MVP

An ASP.NET Core MVC (net8.0) + EF Core + SQL Server MVP inspired by the general UX of
creator marketplaces. Original branding, fictional creators, no proprietary assets.

## Run it

1. Open `NaanoMVP.sln` in Visual Studio (or `cd NaanoMVP` and use the CLI).
2. Confirm the connection string in `appsettings.json` (defaults to LocalDB).
3. Restore & run:
   ```
   dotnet restore
   dotnet run --project NaanoMVP
   ```
4. On first run the app calls `Database.EnsureCreated()` and seeds 18 fictional
   creators, 3 campaigns, campaign-creator links, and campaign metrics automatically —
   no manual migration step is required to see data.

### Optional: proper EF Core migrations
The app currently uses `EnsureCreated()` for a zero-friction demo. If you want real,
versioned migrations instead (e.g. for grading criteria that expects a `Migrations/`
folder), run:
```
dotnet tool install --global dotnet-ef   (if not already installed)
dotnet ef migrations add InitialCreate --project NaanoMVP
dotnet ef database update --project NaanoMVP
```
Then replace `context.Database.EnsureCreated();` in `Program.cs` with
`context.Database.Migrate();`.

## Pages
- `/` — Landing page (hero, stats, how-it-works, featured creators)
- `/Creators` — Marketplace with search, category/platform filters, and sorting
- `/Creators/Details/{id}` — Creator profile
- `/Campaigns/AddToCampaign?creatorId={id}` — Attach a creator to an existing or new campaign
- `/Campaigns/Create` — New campaign form with creator multi-select
- `/Campaigns/Details/{id}` — Campaign detail + attached creators
- `/Dashboard` — Summary cards, spend-by-campaign bar chart, campaign table

## Notes
- No auth, payments, AI, email, or admin panel, per the brief.
- Avatars are CSS-gradient initials (no external image dependency).
- Bootstrap 5 + Bootstrap Icons are loaded from CDN in `_Layout.cshtml`.
