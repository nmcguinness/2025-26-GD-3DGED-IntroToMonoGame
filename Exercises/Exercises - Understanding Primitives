## Exercises - Understanding PrimitiveTypes

### Exercise 1: Basic Recognition (Beginner)
**Objective**: Identify correct PrimitiveTypes for different scenarios

For each scenario, identify the most appropriate PrimitiveType and explain why:

a) Rendering 50 unconnected stars in a night sky  
b) Drawing the outline of a rectangle  
c) Creating a 3D cube mesh  
d) Rendering a curved path that a character will follow  
e) Drawing a selection box around a clicked object  
f) Creating a terrain row from heightmap data  

**Deliverable**: Written answers with justifications

---

### Exercise 2: Vertex Count Calculator (Beginner)
**Objective**: Master primitive count calculations

Implement a utility class that calculates primitive counts:

```csharp
public static class PrimitiveCalculator
{
    /// <summary>
    /// Calculate number of primitives from vertex count
    /// </summary>
    public static int GetPrimitiveCount(int vertexCount, PrimitiveType type)
    {
        // TODO: Implement this
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Calculate number of vertices needed for desired primitive count
    /// </summary>
    public static int GetVertexCount(int primitiveCount, PrimitiveType type)
    {
        // TODO: Implement this
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Validate if vertex count is valid for given PrimitiveType
    /// </summary>
    public static bool IsValidVertexCount(int vertexCount, PrimitiveType type)
    {
        // TODO: Implement this
        throw new NotImplementedException();
    }
}
```

**Test Cases**:
- 12 vertices with TriangleList should return 4 primitives
- 10 vertices with TriangleStrip should return 8 primitives
- 5 vertices with LineList should return INVALID (odd count)
- Calculate vertices needed for 100 triangles using TriangleList vs TriangleStrip

**Deliverable**: Complete implementation with unit tests

---

### Exercise 3: Debug Visualization Tool (Intermediate)
**Objective**: Create a tool to visualize different PrimitiveTypes

Create a class that renders the same set of vertices using all five PrimitiveTypes simultaneously to understand the differences:

```csharp
public class PrimitiveTypeVisualizer
{
    private GraphicsDevice graphicsDevice;
    private BasicEffect effect;
    private VertexPositionColor[] vertices;
    
    public PrimitiveTypeVisualizer(GraphicsDevice device)
    {
        graphicsDevice = device;
        // Create 8 vertices in an interesting pattern
        CreateTestVertices();
    }
    
    private void CreateTestVertices()
    {
        // TODO: Create 8 vertices that will show clear differences
        // between primitive types when rendered
        // Hint: Use a circular or grid pattern
    }
    
    public void Draw(GameTime gameTime)
    {
        // TODO: Render the same vertices 5 times using different
        // PrimitiveTypes, positioned side-by-side for comparison
        
        // Layout:
        // [PointList] [LineList] [LineStrip] [TriangleList] [TriangleStrip]
    }
}
```

**Requirements**:
- Display all 5 types simultaneously
- Label each visualization
- Use different colors for each type
- Add keyboard controls to toggle individual types on/off
- Display vertex count and primitive count for each

**Deliverable**: Complete visualizer that clearly demonstrates differences

---

### Exercise 4: Grid Generator (Intermediate)
**Objective**: Create efficient grid geometry using different PrimitiveTypes

Implement a grid generator that can create the same grid using three different approaches:

```csharp
public class GridGenerator
{
    /// <summary>
    /// Generate grid using LineList (simple but inefficient)
    /// </summary>
    public static VertexPositionColor[] GenerateLineListGrid(
        int width, int height, float spacing)
    {
        // TODO: Create grid where each line is separate
        // Vertices will be duplicated at intersections
    }
    
    /// <summary>
    /// Generate grid using multiple LineStrips (more efficient)
    /// </summary>
    public static VertexPositionColor[] GenerateLineStripGrid(
        int width, int height, float spacing)
    {
        // TODO: Create grid using line strips
        // Challenge: How to handle drawing multiple strips?
    }
    
    /// <summary>
    /// Generate grid using TriangleList with indices (for solid grid)
    /// </summary>
    public static void GenerateTriangleListGrid(
        int width, int height, float spacing,
        out VertexPositionColor[] vertices,
        out short[] indices)
    {
        // TODO: Create solid grid with quads
        // Use indexed drawing for efficiency
    }
    
    /// <summary>
    /// Compare memory usage of different approaches
    /// </summary>
    public static void PrintMemoryComparison(int width, int height)
    {
        // TODO: Calculate and display:
        // - Vertex count for each method
        // - Index count (where applicable)
        // - Total memory usage
        // - Percentage difference
    }
}
```

**Test**: Generate a 10×10 grid and measure vertex counts:
- LineList: ? vertices
- LineStrip: ? vertices  
- TriangleList with indices: ? vertices + ? indices

**Deliverable**: Complete implementation with memory comparison output

---

### Exercise 5: Primitive Converter (Advanced)
**Objective**: Convert geometry between different PrimitiveTypes

Create a system that converts vertex data from one PrimitiveType to another:

```csharp
public static class PrimitiveConverter
{
    /// <summary>
    /// Convert TriangleList to TriangleStrip
    /// Note: Only works for strip-compatible geometry
    /// </summary>
    public static VertexPositionColor[] TriangleListToStrip(
        VertexPositionColor[] triangleList)
    {
        // TODO: Analyze triangles and create strip
        // Challenge: Detect when triangles can't form a valid strip
    }
    
    /// <summary>
    /// Convert TriangleStrip to TriangleList
    /// </summary>
    public static VertexPositionColor[] TriangleStripToList(
        VertexPositionColor[] triangleStrip)
    {
        // TODO: Expand strip into individual triangles
        // Remember: winding order alternates in strips
    }
    
    /// <summary>
    /// Convert LineStrip to LineList
    /// </summary>
    public static VertexPositionColor[] LineStripToList(
        VertexPositionColor[] lineStrip)
    {
        // TODO: Duplicate vertices to create separate segments
    }
    
    /// <summary>
    /// Convert filled triangles to wireframe lines
    /// </summary>
    public static VertexPositionColor[] TrianglesToWireframe(
        VertexPositionColor[] triangles,
        PrimitiveType sourceType)
    {
        // TODO: Extract edges from triangles
        // Return as LineList
        // Challenge: Remove duplicate edges (shared edges)
    }
}
```

**Test Cases**:
1. Convert a quad (2 triangles in TriangleList) to TriangleStrip
2. Convert a 10-triangle strip to TriangleList and verify triangle count
3. Extract wireframe from a cube mesh

**Deliverable**: Complete converter with test suite demonstrating correctness

---

### Exercise 6: Performance Benchmarker (Advanced)
**Objective**: Measure real-world performance differences between PrimitiveTypes

Create a benchmark tool that compares rendering performance:

```csharp
public class PrimitiveBenchmark
{
    public struct BenchmarkResult
    {
        public PrimitiveType Type;
        public int VertexCount;
        public int DrawCalls;
        public float AverageFrameTime;
        public float MemoryUsage;
    }
    
    /// <summary>
    /// Benchmark rendering the same geometry with different types
    /// </summary>
    public static BenchmarkResult[] BenchmarkTerrain(
        GraphicsDevice device,
        int width,
        int height,
        int frames = 1000)
    {
        // TODO: Generate same terrain using:
        // 1. TriangleList with indices
        // 2. TriangleStrip
        // 3. Multiple TriangleStrips with degenerate triangles
        //
        // Measure:
        // - Frame time (average over N frames)
        // - Memory used (vertex + index buffers)
        // - Draw call count
        //
        // Return results for analysis
    }
    
    public static void PrintResults(BenchmarkResult[] results)
    {
        // TODO: Format and display results in a comparison table
    }
}
```

**Requirements**:
- Test with terrain of increasing size (10×10, 50×50, 100×100)
- Measure on both integrated and dedicated GPU (if available)
- Graph results showing performance vs complexity
- Analyze when TriangleStrip becomes beneficial

**Deliverable**: Benchmark tool with results document analyzing findings

---

### Exercise 7: Ribbon Trail Effect (Advanced)
**Objective**: Implement a practical effect using TriangleStrip

Create a ribbon trail effect (like a sword slash or vehicle trail) using TriangleStrip:

```csharp
public class RibbonTrail
{
    private struct TrailPoint
    {
        public Vector3 Position;
        public float Width;
        public Color Color;
        public float Lifetime;
    }
    
    private List<TrailPoint> trailPoints;
    private VertexPositionColor[] stripVertices;
    
    /// <summary>
    /// Add a new point to the trail
    /// </summary>
    public void AddPoint(Vector3 position, float width, Color color)
    {
        // TODO: Add point to trail
        // Rebuild vertex strip
    }
    
    /// <summary>
    /// Update trail (fade out old points)
    /// </summary>
    public void Update(GameTime gameTime)
    {
        // TODO: Age points, remove old ones
        // Fade colors over time
    }
    
    /// <summary>
    /// Build TriangleStrip from trail points
    /// </summary>
    private void RebuildStrip()
    {
        // TODO: Convert trail points to triangle strip
        // Challenge: Handle variable width
        // Challenge: Orient strip to face camera
    }
    
    public void Draw(GraphicsDevice device, Matrix view, Matrix projection)
    {
        // TODO: Render strip with alpha blending
    }
}
```

**Requirements**:
- Trail fades over time (alpha and/or width)
- Maximum trail length (oldest points removed)
- Smooth interpolation between points
- Billboarding (face camera) or world-space orientation option
- Variable width support

**Bonus Challenges**:
- Add texture coordinates for texturing the ribbon
- Implement trail "stretching" based on velocity
- Add secondary ribbon for glow effect

**Deliverable**: Working ribbon trail with demo scene showing a moving object

---

### Exercise 8: Engine Integration (Advanced)
**Objective**: Design a proper abstraction for PrimitiveType in a game engine

Design and implement a mesh system that intelligently handles PrimitiveTypes:

```csharp
public class Mesh
{
    private GraphicsDevice device;
    private VertexBuffer vertexBuffer;
    private IndexBuffer indexBuffer;
    private PrimitiveType primitiveType;
    private int primitiveCount;
    
    /// <summary>
    /// Create mesh with automatic primitive type detection
    /// </summary>
    public static Mesh Create(GraphicsDevice device,
        VertexPositionColor[] vertices,
        short[] indices = null)
    {
        // TODO: Analyze geometry and choose optimal PrimitiveType
        // - If indices present and suitable, use TriangleList
        // - If vertices form strip, use TriangleStrip
        // - etc.
    }
    
    /// <summary>
    /// Optimize mesh for rendering
    /// </summary>
    public void Optimize()
    {
        // TODO: Reorder vertices for vertex cache
        // TODO: Convert to strips where beneficial
        // TODO: Generate indices if not present
    }
    
    /// <summary>
    /// Create debug wireframe mesh
    /// </summary>
    public Mesh CreateWireframe()
    {
        // TODO: Generate LineList mesh from triangle edges
    }
    
    public void Draw(BasicEffect effect)
    {
        // TODO: Render with appropriate primitive type
    }
}

// Extension: Mesh Builder with fluent API
public class MeshBuilder
{
    public MeshBuilder AddVertex(Vector3 position, Color color) { }
    public MeshBuilder AddTriangle(int v0, int v1, int v2) { }
    public MeshBuilder AddQuad(int v0, int v1, int v2, int v3) { }
    public MeshBuilder SetPrimitiveType(PrimitiveType type) { }
    public Mesh Build(GraphicsDevice device) { }
}
```

**Design Considerations**:
- When should PrimitiveType be exposed to users vs hidden?
- How to handle primitive type changes at runtime?
- Should the engine auto-convert between types for optimization?
- How to support both indexed and non-indexed geometry?

**Deliverable**: Complete Mesh system with unit tests and design document explaining architectural decisions
