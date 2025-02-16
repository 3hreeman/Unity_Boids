
### [Boids_with_ECS](https://github.com/3hreeman/Unity_Boids)

### 프로젝트 개요
Unity의 [Unity ECS(Entity Component System)](https://unity.com/kr/blog/engine-platform/on-dots-entity-component-system) 퍼포먼스 테스트를 위한 프로젝트.

<details>
<summary>ECS 개요</summary>

**ECS의 특징**
  
ECS는 Entity(엔티티), Component(컴포넌트), System(시스템)으로 구성된 아키텍처.  
기존의 객체 지향 프로그래밍(OOP) 방식보다 성능과 유지보수 측면에서 많은 이점을 제공함.  
  
① 성능 최적화 (Performance)  
캐시 효율성 향상: 데이터가 연속된 메모리 블록(SoA, Structure of Arrays)으로 저장되므로 CPU 캐시 효율이 극대화됨.  
병렬 처리(Parallel Processing) 최적화: 시스템이 독립적으로 실행될 수 있어 멀티스레딩 환경에서도 쉽게 확장 가능.  

② 유연성 (Modularity & Reusability)  
데이터 기반 아키텍처: 엔티티는 컴포넌트의 집합으로 이루어져 있어 객체 간의 상속 관계가 필요 없음.  
확장성과 유지보수 용이: 특정 기능을 수정하거나 추가할 때 기존 코드에 영향을 주지 않고 독립적으로 구현 가능.  

③ 디커플링 (Decoupling)  
엔티티는 데이터를 포함하지 않으며, 시스템이 해당 데이터를 처리하는 방식으로 동작하여 코드가 모듈화되고 재사용성이 높아짐.  
특정 기능을 다른 프로젝트에서도 쉽게 재사용 가능.  

  
**ECS의 방향성**
   
 ① 차세대 성능 최적화 아키텍처  
 DOTS(Data-Oriented Technology Stack)의 핵심 구성 요소로 활용되며, Unity의 향후 엔진 최적화 방향에서도 중요한 역할을 담당.  
 기존의 객체 지향 방식보다 하드웨어 성능을 극대화할 수 있도록 설계됨.  

 ② 대규모 시뮬레이션 및 게임 개발에 적합  
 수천 개에서 수백만 개의 오브젝트(엔티티)를 효율적으로 관리 가능.  
 AI, 물리 시뮬레이션, 대규모 오픈월드 게임 등에서 ECS의 성능 이점을 극대화할 수 있음.  

 ③ 점진적 도입 가능  
 기존 Unity의 GameObject 및 MonoBehaviour 시스템과 함께 사용할 수 있도록 설계되어 있으며, 점진적인 전환이 가능.  
 기존 프로젝트에서도 ECS의 성능 이점을 부분적으로 활용 가능.  
</details>

#### 레퍼런스 및 테스트 후 비교

[[YUNNONG-Boids](https://github.com/BongYunnong/CodingExpress)] 의 Boids 프로젝트를 바탕으로 하여 ECS로 컨버팅.

![Image](https://github.com/user-attachments/assets/d576d961-cf5d-4444-a894-4fd7750096dc)

1-1) 기존 레퍼런스 프로젝트에서 구현된 Boids 시스템  
각각의 Boid가 개별의 MonoBehaviour형태로 구현되어, 각각의 Update문에서 움직임을 연산.  
오브젝트의 개수가 늘어날 수록 프레임 드랍이 눈에 띄게 발생.  
1000개 이상부터는 원활한 실행이 어려움.  

![Image](https://github.com/user-attachments/assets/359a0262-64e8-40e2-80d3-d9c32f83d9de)

2-1) ECS로 컨버팅하여 재구성한 Boids 시스템  
기존 레퍼런스 프로젝트의 Boid 움직임을 ECS 구조로 컨버팅.  
각 모듈당 10000개씩, 총 2만개의 오브젝트가 평균 CPU 10ms 이하로 쾌적하게 작동하는 것을 확인.  

#### 결론 및 감상
컨버팅 중에 Boids 이동 보정 로직이 일부 수정되어 원본과 완벽히 동일한 형태의 Boids 시스템을 구현하지 못한 것은 아쉬움.  
그럼에도 압도적인 퍼포먼스의 차이를 확인하여 ECS의 데이터 지향적 설계가 추구하는 효율적인 대규모 시스템 구현이라는 방향성을 확인할 수 있었음.  
ECS의 데이터 지향적 구조가 생소하게 느껴졌고, 기존에 익숙한 MonoBahaviour 기반의 시스템과는 구조적으로 많이 다름을 알 수 있었음.  

압도적인 퍼포먼스라는 분명한 장점이 존재하지만, 이미 익숙하여 빠른 테스트와 Iteration이 가능한 기존 Component 시스템의 장점들에 비해 사용하기에 조금 더 까다롭다고 느낌. 하지만 신생 시스템인 만큼 앞으로도 더욱 발전할 수 있기에 추후가 더 기대됨.
