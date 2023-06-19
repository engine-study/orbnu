import { mudConfig, resolveTableId } from "@latticexyz/world/register";

export default mudConfig({

  enums : {
    TerrainType: ["None", "Excavated", "Filled", "Pavement", "Rock", "Mine"],
    ObjectType: ["Axe", "Statumen", "Rudus", "Nucleus", "Pavimentum"],
  },

  tables: {

    MapConfig: {
      keySchema: {},
      dataStruct: false,
      schema: {
        width:"uint32",
        height:"uint32",
        terrain: "bytes",
      },
    },

    Player: "bool",

    Stats: {
      name: "Stats",
      schema: {
        health: "int32",
        attack: "int32",
      },
    },

    Building: {
      name: "Building",
      schema: {
        terrain: "bytes",
      },
    },

    Rock:"bool",
    Road:"bool",
    Obstruction: "bool",

    Position: {
      name: "Position",
      schema: {
        x: "int32",
        y: "int32",
      },
    },

  },
  modules: [
    {
      name: "KeysWithValueModule",
      root: true,
      args: [resolveTableId("Position")],
    },

  ],
});