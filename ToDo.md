# 3D Game Engine Development – ToDo List

## Overview
This document contains a step-by-step development plan of MonoGame content covered in class.  
The work is structured week-by-week and will be developed **live in class** as an introduction to MonoGame and as a foundation for our custom game engine (`GDEngine`).

## Goals
- **Rendering fundamentals** (primitives, W→V→P, clip→NDC→viewport, SRT, FOV/near/far)
- **Cameras & projections** (FPS, Orbit, Perspective↔Orthographic, frustum, culling)
- **Mesh & GPU path** (VertexPositionColor & VertextPositionTexture layout, winding, using vertex and index buffers, DrawIndexedPrimitives, rasterizer)
- **Engineering practices** (patterns: **Strategy**, **Command**, **Builder/Factory**, **Facade**)

## Instructions
- Follow the tasks in order (marked with `[ ]` for incomplete and `[x]` for complete).
- Code is developed interactively in class; update this checklist as we progress.
- Unit tests are implemented in a **separate MSTest project** after the class implementation is stable.
- Each week builds on the previous week.

---

## Week 2 — Warm-up & the Draw Loop

### 2.1 Primitives → SRT → Input → Move Camera
- [x] Generate, build, and run a default MonoGame DirectX/OpenGL project.  
- [x] Understand call-order for **constructor → `Initialize` → `LoadContent` → `Update` → `Draw`**.  
- [x] Render a line and introduce the `world`, `view`, `projection` matrices, `BasicEffect`, and a `VertexPositionColor` array to define primitives.
- [x] Add a rotating `LineList`.
- [x] Discuss where `pass.Apply()` fits before draw calls.


### 2.2 Explore Primitive Types (before SRT)
- [ ] Draw `LineList`, `LineStrip`, `TriangleList`, `TriangleStrip` via `DrawUserPrimitives`.
- [ ] Show vertex counts & `primitiveCount` math; color-code to visualize connectivity.
- [ ] Discuss difference in `S*R*T` order (e.g., `S*R*T` vs `T*R*S`).
- [ ] Add keyboard for scale/rotation speed/reset; mouse scroll for scale or FOV.
- [ ] On-screen/debug readout of current values.
- [ ] Simple camera dolly (Z) with keys (e.g., `Z/X`).

## Week N... 
### FPS & Orbit Cameras, Perspective vs Orthographic, Frustum Basics
- [ ] Implement **FPS camera** (WASD + mouse look).
- [ ] Implement **Orbit camera** (rotate about target, zoom, pan).
- [ ] Toggle **Perspective ↔ Orthographic**; discuss use-cases.
- [ ] Build a **frustum** from `View * Projection` and draw with debug lines.
- [ ] Add **basic culling** (bounding sphere or AABB) and log culled count.

### Vertices & Indices; Winding; Vertex Layouts
- [ ] Define a cube via **positions, normals, uvs** (`VertexPositionNormalTexture`).
- [ ] Build index buffer for 12 triangles; confirm **winding order** (CW/CCW).
- [ ] Add an axis gizmo (`LineList`) to orient the scene.
- [ ] (Optional later) normals verification with simple lighting.

### VertexBuffer / IndexBuffer; Binding; DrawPrimitives / DrawIndexedPrimitives
- [ ] Upload vertices to **`VertexBuffer`**; indices to **`IndexBuffer`**.
- [ ] Replace `DrawUserPrimitives` with **`DrawIndexedPrimitives`** where appropriate.
- [ ] Encapsulate in a **MeshRenderer** helper (`SetMesh`, `SetEffect`, `Render(W,V,P)`).
- [ ] Add rasterizer state toggle: **wireframe ↔ solid** (cull modes if useful).
