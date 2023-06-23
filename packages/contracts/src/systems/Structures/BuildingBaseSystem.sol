// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import { System } from "@latticexyz/world/src/System.sol";
import { getKeysWithValue } from "@latticexyz/world/src/modules/keyswithvalue/getKeysWithValue.sol";
import { Position, PositionTableId, PositionData, Stats } from "../../codegen/Tables.sol";
import { addressToEntityKey } from "../../addressToEntityKey.sol";
import { CostSystem } from "./CostSystem.sol";

contract BuildingBaseSystem is System, CostSystem{ 
   
}
