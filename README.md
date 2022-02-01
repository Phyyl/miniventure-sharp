# miniventure-sharp

Minicraft clone base on [shylor/miniventure](https://github.com/shylor/miniventure). The goal of this project is to translate Miniventure from Java to C#, and add features such as saves, new crafting recipes, new levels, and multiplayer (hopefully).

## Project structure

- Generator: Code for the Java to C# translator
- Source: Initially generated, now refactored, improved source code

## Running the project

- Terminal (requires dotnet-sdk 6): `dotnet run --project .\Source\Miniventure\Miniventure.csproj`
- Visual Studio: Open `.\Source\Miniventure.sln`

## Added features (so far)

- Save game when closing the game
- Throw active item by pressing Z
- Create coal in furnace using 10 wood
