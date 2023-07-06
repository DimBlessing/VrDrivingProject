# VrDrivingProject
Unity VR 운전연습 시뮬레이션
<img width="1009" alt="vd_mainImage" src="https://github.com/DimBlessing/VrDrivingProject/assets/47944912/009b739b-df37-479e-911d-3fa4b50d6ffe">
개발기간: 2022.09 ~ 2022.12

## 팀 소개
정준희 - 전남대학교 소프트웨어공학과
<br>
정희석 - 전남대학교 소프트웨어공학과
<br>
권시온 - 전남대학교 소프트웨어공학과
<br>
장효원 - 전남대학교 소프트웨어공학과
<br>

## 프로젝트 소개
본 시뮬레이션은 운전 경험과 실력이 부족한 운전자를 교육할 수 있는 시스템을 확보하는 것을 목적으로 한다.
<br>
따라서 운전자에게 안전한 운전 습관과 충분한 운전 경험을 제공하기 위한 방법으로써 VR 환경을 활용하여 현실적인 도로 환경을 구현하였으며
<br>
실제 도로연수 등 교육 시스템의 고비용 문제를 개선할 수 있도록 VR 환경과 스티어링 휠 기기(Logitech G29)를 접목시킨 운전 시뮬레이션을
<br>
개발하는 것을 목적으로 프로젝트를 진행한다.
<br>

## 시작 가이드
**Development Requirements**
<br>
Unity Editor 2021.3.12f1 LTS ~
<br>
<br>
**Play Requirements**
<br>
VR Device : Meta Quest 2
<br>
Input(Control) Device: Logitech G29
<br>

## Stacks
**Environment**
<br>
<img src="https://img.shields.io/badge/Meta-0467DF?style=flat-square&logo=unity&logoColor=white"/>
<img src="https://img.shields.io/badge/Visual Studio Code-007ACC?style=flat-square&logo=visualstudiocode&logoColor=white"/>
<img src="https://img.shields.io/badge/GitHub-181717?style=flat-square&logo=github&logoColor=white"/>
<br>
**Development**
<br>
<img src="https://img.shields.io/badge/Unity-000000?style=flat-square&logo=unity&logoColor=white"/>
<img src="https://img.shields.io/badge/CSharp-239128?style=flat-square&logo=Csharp&logoColor=white"/>
<br>
**Communication**
<br>
<img src="https://img.shields.io/badge/Notion-0000000?style=flat-square&logo=notion&logoColor=white"/>
<br>

## 시뮬레이션 화면 구성
| 메인 메뉴  |  사용자 시점   |
| :-------------------------------------------: | :------------: |
|  <img width="329" src="https://github.com/DimBlessing/DimBlessing/assets/47944912/c8f9f1da-5b55-42ad-8816-edd3ff2a354f"/> |  <img width="329" src="https://github.com/DimBlessing/DimBlessing/assets/47944912/d09e107a-da24-4ed3-bcf6-bb76b120e674"/>|  
| 맵   |  평가 / 피드백 메뉴   |  
| <img width="329" src="https://github.com/DimBlessing/DimBlessing/assets/47944912/88fcbe36-a3e8-4bc9-9c01-14d448b1668a"/>   |  <img width="329" src="https://github.com/DimBlessing/DimBlessing/assets/47944912/c9bd7c59-844f-4165-a2d8-750f1e66d06d"/>     |

## 주요 기능
### VR 1인칭 시점 운전 기능
- Meta Quest 2 등 VR기기를 통한 시뮬레이션 내 1인칭 운전석 시점
- Logitech G29 를 사용한 시뮬레이션 내 운전 기능 구현

### 현실적인 도로 주행 환경을 고려한 맵 디자인
- 레벨(난이도)에 따른 도로 유형 및 주행코스 구분
- 사용자 차량 위치를 기준으로 한 주행코스 상 교차로 신호체계 연동
- 향후 날씨(우천 등), 시간대 등 외부 요소를 추가하여 더 다양한 주행 환경을 구현할 계획 

### 이벤트
- 주행코스 내에서 발생가능한 무작위 돌발 상황 이벤트를 배치
- 사용자 차량의 트리거 접촉을 기반으로 한 AI차량 이동 및 사고 상황 구현
- 신호체계와 연계한 횡단보도 보행자(무단횡단 포함)
- 최근 증가하는 PM(Personal Mobility) 관련 이벤트 추가
  
### 평가 및 피드백
- 주행코스 완료/실패 후 사용자에게 평가 및 피드백 창 출력
- 주행 중에 사용자가 올바르게 대응하지 못하여 감점당한 요소를 표시
- 각 감점요소 선택 시 자세한 설명 및 올바른 대응 등에 대한 피드백을 표시

### 참고
git lfs 제한용량 초과로 인해 Level2.unity(메인 씬) 별도 첨부
https://drive.google.com/file/d/1vPq2pt9vzyW2FP9ZzWlfl7iZhC_tJSk1/view?usp=share_link












