# Unity_Boids
---
Unity의 [Unity ECS(Entity Component System)](https://unity.com/kr/blog/engine-platform/on-dots-entity-component-system)을 테스트하기 위한 프로젝트입니다.

[[YUNNONG-Boids](https://github.com/BongYunnong/CodingExpress)]프로젝트를 기반으로 하여, 해당 구조를 바탕으로 ECS로 컨버팅하는 작업을 거쳤습니다.

![Image](https://github.com/user-attachments/assets/d576d961-cf5d-4444-a894-4fd7750096dc)

1-1) 기존 레퍼런스 프로젝트에서 구현된 Boids 시스템

![Image](https://github.com/user-attachments/assets/359a0262-64e8-40e2-80d3-d9c32f83d9de)

2-1) ECS로 컨버팅하여 재구성한 Boids 시스템

ECS 시스템에 대한 이해도가 부족하여 기존 프로젝트에서 보이던 Trail효과나, 로직 변경 중에 일어난 Boids 이동 보정 로직이 변경되어 원본과 완벽히 동일한 형태의 Boids 시스템을 구현하지 못한 것은 아쉬운 부분입니다.
다만, 기존 Update 구조에서는 1000개만 넘어가도 프레임 드랍이 심각하게 일어났던 것에 비해, 각 모듈당 10000개씩, 총 2만개의 오브젝트가 평균 CPU 10ms 이하로 쾌적하게 작동하는 것을 확인할 수 있었습니다.
