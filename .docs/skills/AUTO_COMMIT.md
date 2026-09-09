# 자동 커밋

사용자가 `자동 커밋`, `커밋 올려줘`, `변경사항 커밋`, `커밋 분할`처럼 커밋을 명시적으로 요청했을 때만 사용한다.

## 절차

1. `git status`, `git diff`, `git diff --staged`로 변경사항과 충돌 여부를 확인한다.
2. 독립적으로 되돌릴 수 있는 기능 단위로 파일을 묶는다. `.meta` 파일과 테스트는 대응 원본 구현과 반드시 같은 묶음에 포함한다.
3. 커밋 계획(메시지와 포함 파일)을 사용자에게 제시하고 승인받는다.
4. 승인된 묶음만 순서대로 stage·commit하고, 각 커밋 결과를 확인한다.
5. 커밋 완료 뒤 해시와 메시지를 요약한다. push는 별도 요청일 때만 한다.

## 메시지 규칙

- 형식: `<type>(<scope>): <한글 설명>` 또는 `<type>: <한글 설명>`
- `type`과 선택적인 `scope`는 영어, 설명은 한글로 작성한다.
- 현재 저장소의 기존 형식을 존중하므로 scope를 강제하지 않는다.
- type: `feat`, `fix`, `refactor`, `test`, `docs`, `chore`, `perf`
- scope 예시: `Player`, `Prologue`, `Scene`, `Input`, `Core`, `UI`, `Audio`, `Build`, `Docs`, `Harness`, `Test`

## Unity 점검

- `Library/`, `Temp/`, `Logs/`, `obj/`, 생성된 솔루션 파일은 커밋 대상에서 제외한다.
- 새·이동·삭제된 `Assets` 원본은 대응 `.meta` 파일까지 포함됐는지 확인한다.
- `ProjectSettings/`와 `Packages/` 변경은 의도와 영향 범위를 커밋 계획에 명시한다.
- 커밋 전 검증은 [UNITY_VERIFY.md](UNITY_VERIFY.md)를 따른다.
