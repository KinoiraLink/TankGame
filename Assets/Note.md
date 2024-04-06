# TileMap
> [kenney: 一个非常可爱的资源站](https://www.kenney.nl/)  
> [superpowers：一个非常丰富的开源资源包](https://github.com/sparklinlabs/superpowers-asset-packs/tree/master)

重新设置瓦片
```yml
Pixels Per Unit: 128
Filter Mode: Point
Compression: None

```

下载 Tilemap 包

层级窗口种创建 Tilemap Rectangular Ground

创建一个调色板 Ground

barricadeMetal.png、barricadeWood.png 同样设置为瓦片同样的设置
```yml
Pixels Per Unit: 56
Filter Mode: Point
Compression: None

```
创建一个调色板 Obstacles

层级窗口种创建 Tilemap Rectangular Obstacles
```yml
Scale: 0.5 0.5 1;
```
![](../images/2024-04-06-12-28-32.png)

创建一个调色板 Details

向 Obstacles 添加 TileMap Collider 2D、Composite Collider 2D

`Used By Composite: true`、`Body Type: Static`

搭建场景