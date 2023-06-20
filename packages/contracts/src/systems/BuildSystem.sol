// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;
import { System } from "@latticexyz/world/src/System.sol";
import { MapConfig, Position, Building, PositionTableId, Player, PositionData } from "../codegen/Tables.sol";
import { getKeysWithValue } from "@latticexyz/world/src/modules/keyswithvalue/getKeysWithValue.sol";
import { addressToEntityKey } from "../addressToEntityKey.sol";

contract BuildSystem is System {

  function build(int32 x, int32 y) public {
    bytes32 playerEntity = addressToEntityKey(address(_msgSender()));
    require(!Player.get(playerEntity), "already spawned");

    Player.set(playerEntity, true);
    Position.set(playerEntity, x, y);
    
    // Health.set(playerEntity, 100);
    // Damage.set(playerEntity, 10);
  }

  function traversedPositions(
    PositionData memory start,
    PositionData memory end
  ) internal pure returns (PositionData[] memory) {
    int32 changeX = end.x - start.x;
    int32 changeY = end.y - start.y;

    int32 signX = changeX >= 0 ? int32(1) : int32(-1);
    int32 signY = changeY >= 0 ? int32(1) : int32(-1);

    int32 change = changeX != 0 ? changeX : changeY;
    int32 sign = changeX != 0 ? signX : signY;

    uint32 arraySize = uint32((change + sign) * sign);
    PositionData[] memory positions = new PositionData[](arraySize);

    int32 xIndex = start.x;
    int32 yIndex = start.y;

    uint256 index = 0;

    while (xIndex != end.x + signX && changeX != 0) {
      positions[index] = PositionData(xIndex, start.y);
      index++;
      xIndex += signX;
    }

    while (yIndex != end.y + signY && changeY != 0) {
      positions[index] = PositionData(start.x, yIndex);
      index++;
      yIndex += signY;
    }

    return positions;
  }

  function abs(int x) private pure returns (int) {
    return x >= 0 ? x : -x;
  }

}