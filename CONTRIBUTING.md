# 🤝 Contributing to The Arcane West

---

## 📋 Prima di iniziare

1. Leggi il [README.md](./README.md) completo
2. Configura il tuo ambiente seguendo [Setup iniziale](./README.md#-setup-iniziale)
3. Verifica di avere accesso al repository GitHub

---

## 🎯 Prendere in carico una task

### 1. Su Trello

1. Vai nella board del progetto
2. Scegli una task dallo status **"TODO"** o **"BACKLOG"**
3. Assegnala a te stesso/a
4. Sposta la task in **"DOING"**

### 2. Sul tuo PC
Crea un nuovo branch (commit e push di inizializzazione) (vedi "convenzione naming del branch")
```bash
# Aggiorna development
git switch development
git pull --rebase

# Crea e passa al nuovo branch
git switch -c feature/abc123def-task-name
```

### 3. Su Trello
Lega il branch alla card di Trello (verificare se si può fare automaticamente con naming conventions speciali)
Apri la card di trello. Sotto Power-Up -> GitHub -> Allega branch... dovresti trovare il branch appena pushato. (Nota: a volte Trello ci mette un po' ad aggiornare i branch disponibili)

### Convenzione naming dei branch

Quando crei un branch manualmente, segui questa convenzione:

```
{tipo}/{card-id}-{task-name}

{tipo} -> Vedi "Tipi di branch"
{card-id} -> Apri la card. L'URL dovrebbe essere simile a "https://trello.com/c/{card-id}/00-{task-name}"
```

#### Tipi di branch

| Prefisso    | Scopo                      | Esempio                                    |
|-------------|----------------------------|--------------------------------------------|
| `feature/`  | Nuove funzionalità         | `feature/CU-abc123def_player-dash-ability` |
| `fix/`      | Correzione bug             | `fix/CU-abc124_jump-animation-glitch`      |
| `refactor/` | Refactoring codice         | `refactor/CU-abc125_gas-setup-cleanup`     |

**Regole**:
- Il branch **deve sempre staccarsi da `development`**, mai da `main`
- Usa trattini `-` per separare ID dal nome
- Usa trattini `-` per separare le parole nel nome, come fosse un whitespace

---

## 💻 Durante lo sviluppo

### Regola base: rebase frequente

**Almeno 2 volte al giorno**, aggiorna il tuo branch con `dev`:

```bash
git switch dev
git pull --rebase
git switch feature/abc123def-task-name
git rebase dev
```

---

## 📝 Commit guidelines

### Formato

```
<tipo>: <descrizione breve>
(<task-id>)
```
---

## 🔀 Pull Request

### Quando aprirla

- ✅ Feature completa e testata
- ✅ Nessun errore di compilazione
- ✅ Progetto si apre senza warning critici
- ✅ Asset seguono naming conventions
- ✅ Codice commentato dove necessario

### Come aprirla

```bash
# Push del branch
git push origin feature/abc123def-task-name
```

**Su GitHub**:
1. Click su "Compare & pull request"
2. **Base**: `development` (NON `main`)
3. **Titolo** (italiano): `[abc123def] Breve descrizione`
4. Compila il template (Il file template si trova in `.github/pull_request_template.md` ed è suggerito seguirlo per tutte le PR, a meno che non si tratti di una PR estremamente banale (es. correzione di un typo nella documentazione).
5. Assegna almeno 1 reviewer

### Strategia di merge

#### Il workflow ideale: Rebase + Merge --no-ff

La nostra strategia preferita è **rebase-before-merge-commit**, che combina:
1. **Rebase** della feature branch su `dev` (per una base pulita)
2. **Merge commit** (--no-ff) per preservare il contesto della feature

Questo approccio offre:
- ✅ Storia lineare sulla feature branch
- ✅ Merge commit che delimita chiaramente ogni feature
- ✅ `git log --first-parent` pulito con solo i merge commit
- ✅ Facile rollback di intere feature
#### Come procedere

**Opzione A: CLI (raccomandato per repository master)**

```bash
# 1. Assicurati che dev sia aggiornato
git checkout dev
git pull --rebase

# 2. Vai sulla feature branch e fai rebase
git checkout feature/abc123_task-name
git rebase dev

# 3. Risolvi eventuali conflitti
# Se ci sono conflitti: risolvi, poi git add <file> e git rebase --continue

# 4. Verifica che tutto compili e funzioni

# 5. Torna su dev e fai merge --no-ff
git checkout dev
git merge --no-ff feature/abc123_task-name

# 6. Push di dev
git push origin dev

# 7. Su GitHub, chiudi manualmente la PR (sarà marcata come merged automaticamente)
```

**Opzione B: Via GitHub UI (accettabile per semplicità)**

Se preferite usare l'interfaccia GitHub per comodità:

1. Fai **rebase locale** prima di aprire/aggiornare la PR:
   ```bash
   git checkout feature/abc123_task-name
   git rebase dev
   git push --force-with-lease
   ```

2. Su GitHub, usa **"Create a merge commit"** (l'unica opzione abilitata)

3. **NON usare** il bottone "Update branch" nell'UI di GitHub (fa un merge, non un rebase)

---

## 🔒 Branch protection rules

### `dev`
- 🔒 **Protetto**: no push diretto (solo @Marcutyo può fare push diretto in casi particolari)
- ✅ Richiede **1+ approval**
- ✅ Merge strategy: **Rebase-before-Merge-Commit** (rebase locale + merge --no-ff, vedi [Strategia di merge](#il-workflow-ideale-rebase--merge---no-ff))
- 🎯 Target per tutte le feature in sviluppo

### `main`
- 🔒 **Protetto**: no push diretto
- ✅ Richiede **4+ approval** (team programming quasi completo)
- ✅ Solo per release stabili
- ✅ Merge strategy: **Rebase-before-Merge-Commit** (come dev)
- 🎯 Release "al pubblico"

---

## 📚 Risorse extra

### Git
- [Git Cheat Sheet](https://education.github.com/git-cheat-sheet-education.pdf)
- [How to Write Good Commit Messages](https://cbea.ms/git-commit/)
- [Understanding Git Rebase](https://git-scm.com/book/en/v2/Git-Branching-Rebasing)
