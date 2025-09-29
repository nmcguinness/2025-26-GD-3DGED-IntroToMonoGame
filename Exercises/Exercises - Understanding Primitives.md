## Exercises - PrimitiveType in 3D Graphics in MonoGame (or DirectX or OpenGL)

### Exercise 1: Basic Recognition
**Description**: For each scenario, pick the most appropriate `PrimitiveType` and justify briefly.  
a) 50 unconnected stars in a night sky  
b) Outline of a rectangle  
c) A 3D cube mesh  
d) A curved path a character will follow  
e) A selection box around a clicked object  
f) A single terrain **row** from heightmap data  
**Difficulty**: Easy

**Hint**: Think “independent vs connected”, and whether you’re drawing **lines** or **filled triangles**.

---

### Exercise 2: Vertex/Primitive Count Calculator
**Description**: Implement a static helper that converts between **vertex count** and **primitive count** for each topology, and validates counts. Add simple unit tests.  
**Difficulty**: Easy

```csharp
public static class PrimitiveCalculator
{
    public static int GetPrimitiveCount(int vertexCount, PrimitiveType type) => type switch
    {
        PrimitiveType.LineList      => vertexCount / 2,
        PrimitiveType.LineStrip     => vertexCount - 1,
        PrimitiveType.TriangleList  => vertexCount / 3,
        PrimitiveType.TriangleStrip => vertexCount - 2,
        _ => 0
    };

    public static int GetVertexCount(int primitiveCount, PrimitiveType type) => type switch
    {
        PrimitiveType.LineList      => primitiveCount * 2,
        PrimitiveType.LineStrip     => primitiveCount + 1,
        PrimitiveType.TriangleList  => primitiveCount * 3,
        PrimitiveType.TriangleStrip => primitiveCount + 2,
        _ => 0
    };

    public static bool IsValidVertexCount(int vertexCount, PrimitiveType type) => type switch
    {
        PrimitiveType.LineList      => vertexCount % 2 == 0,
        PrimitiveType.LineStrip     => vertexCount >= 2,
        PrimitiveType.TriangleList  => vertexCount % 3 == 0,
        PrimitiveType.TriangleStrip => vertexCount >= 3,
        _ => false
    };
}
```

**Test ideas**:  
- 12 verts + `TriangleList` ⇒ 4 prims  
- 10 verts + `TriangleStrip` ⇒ 8 prims  
- 5 verts + `LineList` ⇒ **invalid**  
- Vertices needed for 100 triangles: `TriangleList` vs `TriangleStrip`.

---

### Exercise 3: Crosshair with `LineList`
**Description**: Draw two independent line segments crossing at the origin (red horizontal, green vertical, length 2 units) using `DrawUserPrimitives` + `LineList`. Print your computed primitive count.  
**Difficulty**: Easy

```csharp
var verts = new VertexPositionColor[]
{
    new(new Vector3(-1, 0, 0), Color.Red),   new(new Vector3( 1, 0, 0), Color.Red),
    new(new Vector3( 0,-1, 0), Color.Green), new(new Vector3( 0, 1, 0), Color.Green),
};
int prims = verts.Length / 2; // 2 verts/line
```

**Hint**: Lists consume vertices in fixed-size groups; strips reuse prior vertices.

---

### Exercise 4: Polyline & Closed Loop with `LineStrip`
**Description**: Make a 5-point zig-zag `LineStrip` across the X-axis. Then **close** the loop by repeating the first vertex at the end. Compare primitive counts (open vs closed).  
**Difficulty**: Moderate

```csharp
var open = new [] {
    V(-2,0,0, Color.Yellow), V(-1,0.7f,0, Color.Orange),
    V(0,-0.5f,0, Color.Red), V(1,0.8f,0, Color.Magenta),
    V(2,0,0, Color.Purple),
};
int primsOpen = open.Length - 1;
```

**Hint**: `LineStrip` doesn’t close automatically—**append the first vertex**.

---

### Exercise 5: Quad from `TriangleList` (Color Blend)
**Description**: Build a centered quad from **two triangles** with `TriangleList`. Give each corner a distinct color and observe interpolation across the surface.  
**Difficulty**: Moderate

```csharp
var v0 = new VertexPositionColor(new Vector3(-1,  1, 0), Color.Red);
var v1 = new VertexPositionColor(new Vector3( 1,  1, 0), Color.Green);
var v2 = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Blue);
var v3 = new VertexPositionColor(new Vector3( 1, -1, 0), Color.Yellow);
var verts = new[] { v0, v1, v2,  v2, v1, v3 };
int prims = verts.Length / 3;
```

**Hint**: Enable `_effect.VertexColorEnabled = true` to see colors.

---

### Exercise 6: Ribbon with `TriangleStrip`
**Description**: Create a vertical “ribbon” using `TriangleStrip` with at least 8 vertices (alternating bottom/top). Compute `N-2` for primitive count.  
**Difficulty**: Moderate

**Hint**: With culling enabled, strip **winding flips** each triangle; consider `RasterizerState.CullNone` while learning.

---

### Exercise 7 (Challenge): PrimitiveType Visualizer
**Description**: Render the **same vertex set** four times side-by-side—once for each topology (`LineList`, `LineStrip`, `TriangleList`, `TriangleStrip`). Label each, show **vertex count** and **primitive count**, and add keys (e.g., 1–4) to toggle each panel. Use only `DrawUserPrimitives` and `VertexPositionColor`.  
**Difficulty**: Challenge

**Requirements**:  
- Distinct colors per topology; clear on-screen labels.  
- A vertex pattern that exposes differences (e.g., zig-zag arc).  
- No vertex/index buffers; reuse a single array.  

**Hint**: Reposition panels by applying a translation to your vertex positions or adjusting `World`.

---

### Exercise 8 (Challenge): Grid Generator – Lists & Strips Only
**Description**: Write methods to generate an **XY grid** two ways *without indices*:  
1) `GenerateLineListGrid(width, height, spacing)` – every grid edge is an independent `LineList` segment (vertex duplication is fine).  
2) `GenerateLineStripGrid(width, height, spacing)` – build the same grid as a set of **multiple** `LineStrip`s (one per row and one per column).  
3) *(Optional)* `GenerateTriangleListGrid(width, height, spacing)` – a **solid** grid using `TriangleList` only (6 verts per cell).  
Print vertex counts for each approach and briefly discuss memory trade-offs.  
**Difficulty**: Challenge

**Hint**: You’re comparing vertex counts, **not** using index buffers. Expect the `TriangleList` version to use many more vertices than an indexed approach.

---

### Utility (for exercises)
```csharp
static VertexPositionColor V(float x, float y, float z, Color c)
    => new VertexPositionColor(new Vector3(x, y, z), c);
```
