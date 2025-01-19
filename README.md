# UnityGameTaskPublic
게임 클라이언트 개발 온보딩 과제
간단한 2D 게임


# ⚙개발 환경
* ``C#``
* ``Unity 6000.027f1``
* ``Visual Studio 2022 콘솔``

# 🕹기능 구현 
1. 2D 타일맵 구현
2. 플레이어, 몬스터 구현
3. 플레이어 공격 구현
4. HP바 구현
5. 유닛 정보 팝업창 구현

# ✏️사용 기술
1. 어드레서블을 사용한 리소스 관리
2. 플레이어, 몬스터를 BaseController, CreatureController로 상속구조 적용
3. Physics.Overlap을 사용하여 적을 탐지하고 공격
4. FSM 적용
5. ResourceManager로 프리팹을 로드 => ObjectManager로 로드된 오브젝트들 생성 및 관리 => PoolManager에서 오브젝트풀로 관리
