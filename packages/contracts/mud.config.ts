import { mudConfig, resolveTableId } from "@latticexyz/world/register";

export default mudConfig({

  tables: {

    MapConfig: {
      keySchema: {},
      dataStruct: false,
      schema: {
        width: "uint32",
        height: "uint32",
        terrain: "bytes",
      },
    },

    //u
    Player: "bool",

    //negative health is a thing ("overkill effects")
    Health: "int32",

    //negative attack is healing
    Attack: "int32",

    Population: "uint32",
    Workers: "uint32",

    Stats: {
      name: "Stats",
      schema: {
        health: "int32",
        attack: "int32",
      },
    },

    Resources: {
      name: "Resources",
      schema: {
        population: "int32",
        energy: "int32",
      },
    },

    Trigger: "uint32",
    TurnNumber: "uint32",
    BlockInfo: "uint32",

    // Plot: {
    //   name: "Plot",
    //   schema: {
    //     buildings: "uint256[]", // dynamic array of entities (that should have Action component)
    //     // barArray: "uint256[2]" // Store also supports static arrays
    //   },
    // },

    Building: {
      name: "Building",
      schema: {
        buildingName: "string",
      },
    },

    Rock: "bool",
    Road: "bool",
    Obstruction: "bool",

    Position: {
      name: "Position",
      schema: {
        x: "int32",
        y: "int32",
      },
    },

    // AnimalTypeTable: "AnimalType",

  },

  enums: {
    TerrainType: ["None", "Dust", "Grass", "Forest", "Sand"],
    NatureType: ["None", "SurfaceOre", "Uranium"],
    BuildingType: ["Capital", "City", "CommLink", "Reactor", "Deterrent", "Storage"],
    AnimalType: ["NONE", "DOG", "CAT", "SQUIREL"],
  },


  modules: [
    {
      name: "KeysWithValueModule",
      root: true,
      args: [resolveTableId("Position")],
    },
    {
      name: "KeysWithValueModule",
      root: true,
      args: [resolveTableId("Building")],
    },

  ],
});