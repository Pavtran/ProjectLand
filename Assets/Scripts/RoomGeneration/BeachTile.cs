using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BeachTile : BiomeTile
{
	// randomization constants
	public int bloomNum = 100;
	public RoomManager.Count bloomSize = new RoomManager.Count(3, 7);
	public float bloomTightness = 0.4f;

	public const int BiomeNumber = 5;

	public override void RandomBlocking(List<Tile> region) {

		base.RandomBlocking(region);

		for (int num = 0; num < bloomNum; num++) {

			Tile randomTile = region[Random.Range(0, region.Count)];
			BlockingExplosion(randomTile.x,
			                  randomTile.y,
			                  Random.Range (this.bloomSize.minimum, this.bloomSize.maximum + 1),
			                  new TilePlacer(this.PlaceWaterTiles));
		}
//		this.PerlinGenerator(region,
//		                     new TilePlacer(this.PlaceWaterTiles),
//		                     .4f,
//		                     .05f);
	}

	public override int getBiomeNumber() {
		return 5;
	}

	public void PlaceWaterTiles(int x, int y) {
		this.GetComponent<RoomManager>().SetGroundTile(this.getBlockingTile(), x, y);
		this.tileMap[x,y].blocking = true;
	}
}