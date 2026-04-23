# 项目进度 - Sweeper: Awakening

## Sprint 0：原型验证与基础移动 ✅ 已完成

**日期**: 2026-04-24

### 交付物

| 模块 | 文件 | 状态 |
|------|------|------|
| 机器人移动控制 | `Assets/Scripts/RobotController.cs` | ✅ |
| 相机跟随 | `Assets/Scripts/FollowCamera.cs` | ✅ |
| 3D 激光扫描特效 | `Assets/Scripts/LidarDisplay.cs` | ✅ |
| 2D LIDAR 小地图 UI | `Assets/Scripts/SimpleLidarUI.cs` | ✅ |

### 场景搭建 (SampleScene)

| 对象 | 描述 |
|------|------|
| Floor | 地面 20x0.2x20 |
| Wall_Back / Front / Right / Left | 四面墙壁 |
| Robot | 圆柱体 (scale 1, 0.3, 1)，含 Rigidbody + RobotController |
| Main Camera | 含 FollowCamera，目标指向 Robot |
| LidarCanvas | 右下角 200x200 LIDAR 显示 |
| LidarDisplay | RawImage + SimpleLidarUI |
| Obstacle_Box / Table / Column | 3 个障碍物 |

### 已修复问题

- **Input System 兼容**: RobotController 原使用旧版 `Input.GetAxis()`，因项目启用了新 Input System 导致 `InvalidOperationException`。已改用 `UnityEngine.InputSystem.Keyboard.current` 读取 WASD 键盘输入。

### 操作方式

- **WASD** 控制机器人移动
- 相机平滑跟随
- 3D 场景中绿色激光射线实时扫描
- 右下角 LIDAR UI 显示点阵扫描图

---

## 待开发

### Sprint 1：激光雷达系统与无视觉体验（核心 MVP）

- 实现 2D SLAM 模拟算法（Grid Map）
- 制作扫描线 Shader
- 搭建第一个测试沙盒房间
- 验收：只看点云图能绕开障碍物完成 80% 区域清扫

### Sprint 2：视觉系统与物体识别

- 制作高清房间材质与环境光
- 实现 DetectableObject 组件脚本
- 编写剧情脚本：获得照片 -> 解锁视觉

### Sprint 3：扩展移动能力

- 重构移动控制器：支持 Surface Normal 检测（爬墙）
- 实现短时浮空（飞行）及燃料 UI
- 设计垂直平面的清扫判定（擦窗逻辑）

### Sprint 4：人形化与剧情高潮

- 角色模型变形动画融合
- 实现简易抓取系统
- 制作结局关卡
