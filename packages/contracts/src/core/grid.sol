// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

function mooreNeighborhood(PositionData memory center) internal pure returns (PositionData[] memory) {
  PositionData[] memory neighbors = new PositionData[](9);
  uint256 index = 0;

  for (int32 x = -1; x <= 1; x++) {
    for (int32 y = -1; y <= 1; y++) {
      neighbors[index] = PositionData(center.x + x, center.y + y);
      index++;
    }
  }

  return neighbors;
}
