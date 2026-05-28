# 🎮 The Arcane West

**The Arcane West** è un vampire survivors-like sviluppato in Unity da **FunGunStick Games**.

---

## 👥 Il team

**FunGunStickGames** è composto da 4 persone con il contributo occasionale di esterni:

- Riccardo Melone
- Lorenzo De Martino
- Simone Orlini
- Martina Tenani
  
- Alessandro Paltrinieri (Game Designer - esterno)

---

## 🛠 Tech stack

- **Engine**: Unity 6000.4.6f1
- **Linguaggi**: C#
- **Version Control**: Git
- **Project Management**: Trello
- **Repository**: GitHub
- **Piattaforma target**: PC

---

## 🚀 Setup iniziale

### Prerequisiti

1. **Unity 6000.4.6f1** installato via Unity Hub
2. **Git** (versione 2.30+)
3. **Visual Studio 2026** / **JetBrains Rider**
4. Account **Trello** configurato
5. Account **GitHub** con accesso al repository

### Prima configurazione

```bash
# 1. Clona il repository
git clone git@github.com:LoyD-Dema/LuginProject.git
cd LuginProject

# 2. Apri il progetto
# Apri Unity Hub e apri il progetto.
```

---

## 🔄 Workflow di sviluppo

Il nostro workflow combina Gitflow e Trunk-Based Development con branch short-lived e rebase frequenti.

### Quick start

1. Prendi una task da Trello
2. Assegna il tuo utente in Membri per prendere in carico la task
3. Crea branch da `dev` con prefisso appropriato (`feature/`, `fix/`, `refactor/`)
4. Sviluppa e commita frequentemente
5. Rebase su `dev` (incorpora le modifiche fatte su dev)
6. Apri PR verso `dev` quando completo
7. Dopo 1+ approval, merge

---

## 📐 Convenzioni

### Naming conventions

Seguiamo le [convenzioni standard di Unity](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity).

| Category | Convention | Example | Notes |
|---|---|---|---|
| Classes | PascalCase | `PlayerController` | Use nouns or noun phrases |
| Interfaces | Prefix with `I` + PascalCase | `IDamageable` | Interface names should describe capability |
| Methods | PascalCase with verbs | `MovePlayer()` | Methods should describe actions |
| Bool Methods | Ask a question | `IsGrounded()` | Return `true/false` clearly |
| Public Fields | PascalCase | `MoveSpeed` | Unity commonly uses PascalCase for public fields |
| Private Fields | camelCase or prefixed | `moveSpeed` / `m_moveSpeed` | Prefixes are optional but common |
| Constants | `k_` + PascalCase | `k_MaxHealth` | Helps distinguish constants |
| Static Variables | `s_` + PascalCase or camelCase | `s_Instance` | Optional convention |
| Local Variables | camelCase | `currentHealth` | Keep names descriptive |
| Parameters | camelCase | `targetPosition` | Same style as local variables |
| Boolean Variables | Verb-based camelCase | `isDead`, `hasWeapon` | Makes conditions readable |
| Enums | PascalCase singular nouns | `WeaponType` | Enum values also use PascalCase |
| Bitwise Enums (`[Flags]`) | PascalCase plural nouns | `MovementDirections` | Represents combinations |
| Events | Verb phrases | `DoorOpened` | Describe what happened |
| Event Handlers | Prefix with `On` | `OnDoorOpened()` | Common Unity/C# event style |
| Event Observer Methods | `Subject_EventName` | `GameEvents_DoorOpened()` | Optional observer naming pattern |
| Namespaces | PascalCase | `MyGame.UI` | Match project/folder structure |
| Files | Match MonoBehaviour name | `PlayerController.cs` | One MonoBehaviour per file |
| Properties | PascalCase | `CurrentHealth` | Use properties over public fields when possible |
| Serialized Private Fields | Private naming + `[SerializeField]` | `[SerializeField] private float m_speed;` | Common Unity best practice |

---

## 🆘 Problemi comuni
Riportare qui eventuali problemi comuni dovessero presentarsi.

---
