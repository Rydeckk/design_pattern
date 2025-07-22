# Robot Factory

Ce projet implémente une usine de fabrication de robots en utilisant des concepts de programmation orientée objet et des design patterns. L'application permet de gérer un inventaire de pièces, de vérifier la disponibilité des stocks, de produire des robots, et de générer des instructions d'assemblage.

# Release 2.0 - Design Patterns Implementation

## Fonctionnalités

### Fonctionnalités Release 1.0

- **Gestion de l'inventaire** : Ajout, affichage et mise à jour des stocks de pièces.
- **Vérification des commandes** : Vérifie si les pièces nécessaires pour produire des robots sont disponibles.
- **Production de robots** : Génère des robots en fonction des commandes et met à jour les stocks.
- **Instructions d'assemblage** : Génère les étapes nécessaires pour assembler les robots.

### Nouvelles Fonctionnalités Release 2.0

- **Système de catégories** : Classification des pièces et robots par usage (Généraliste, Domestique, Industriel, Militaire)
- **Validation des contraintes** : Validation automatique des règles de compatibilité entre catégories
- **Templates personnalisés** : Création de nouveaux types de robots via la commande ADD_TEMPLATE
- **Architecture extensible** : Implémentation de 3 design patterns pour une meilleure maintenabilité

### Nouvelles Fonctionnalités Release 3.0

- **Gestion de commandes** : Ajout du module de Gestion de commandes
- **Gestion des entrées et sorties** : Ajout du module de Gestion des entrées/sorties et de l'instruction RECEIVE

## Robots disponibles

### Robots Release 1.0

- **RobotI** : HeartI, GeneratorI, GripperI, WheelI
- **RobotII** : HeartII, GeneratorII, GripperII, WheelII
- **RobotIII** : HeartIII, GeneratorIII, GripperIII, WheelIII

### Nouveaux Robots Release 2.0 (Par catégorie)

- **XM-1** (Militaire) : Core_CM1, Generator_GM1, Arms_AM1, Legs_LM1
- **RD-1** (Domestique) : Core_CD1, Generator_GD1, Arms_AD1, Legs_LD1
- **WI-1** (Industriel) : Core_CI1, Generator_GI1, Arms_AI1, Legs_LI1

## Commandes disponibles

Voici les commandes que vous pouvez utiliser dans l'application :

### 1. `STOCKS`

Affiche l'inventaire actuel.

**Exemple :**
`STOCKS`

### 2. `NEEDED_STOCKS`

Affiche les pièces nécessaires pour produire une quantité donnée de robots.

**Exemple :**
`NEEDED_STOCKS 2 RobotI, 3 RobotII`

### 3. `INSTRUCTIONS`

Génère les instructions d'assemblage pour une quantité donnée de robots.

**Exemple :**
`INSTRUCTIONS 2 RobotI, 3 RobotII`

### 4. `VERIFY`

Vérifie si les pièces nécessaires pour produire une quantité donnée de robots sont disponibles.

**Exemple :**
`VERIFY 2 RobotI, 3 RobotII`

### 5. `PRODUCE`

Produit les robots en fonction des quantités spécifiées et met à jour les stocks.

**Exemple :**
`PRODUCE 2 RobotI, 3 RobotII`

### 6. `ADD_TEMPLATE` (Nouveau Release 2.0)

Crée un nouveau template de robot personnalisé.

**Format :**
`ADD_TEMPLATE TEMPLATE_NAME, Piece1, Piece2, ...`

**Exemple :**
`ADD_TEMPLATE CustomRobot, Core_CM1, Arms_AM1`

**Validation :** Le template est automatiquement validé selon les règles de compatibilité des catégories.

### 7. `ORDER`

Renvoie un uuid si la commande est valide

**Exemple :**
`ORDER 1 RobotI, 1 RobotII`

### 8. `SEND`

Envoie de la commande

**Exemple :**
`SEND c1b2cdaf-6c5c-4c85-a7e6-077bc8d58ae8, 1 RobotI, 1 RobotII`

### 9. `RECEIVE`

Liste de pièces, assemblages ou robots à ajouter au stock.

**Exemple :**
`RECEIVE 2 RobotI, 3 RobotII`

### 10. `IMPORT`

Import de fichier sous format txt, json ou xml depuis le dossier Files

**Exemple :**
`IMPORT import.json`

### 11. `EXPORT`

Export de fichier sous format txt, json ou xml et l'enregistre dans le dossier Files

**Exemple :**
`EXPORT export.json`

### 12. `EXIT`

Quitte l'application.

## Design Patterns Implémentés

### 1. **Abstract Factory Pattern**

**Localisation :** `src/Robot_factory/Factories/`

**Objectif :** Créer des robots selon leur catégorie (Militaire, Domestique, Industriel) avec des règles de validation spécifiques.

**Classes principales :**

- `IRobotFactory` : Interface commune pour toutes les usines
- `MilitaryRobotFactory` : Usine pour robots militaires (XM-1)
- `DomesticRobotFactory` : Usine pour robots domestiques (RD-1)
- `IndustrialRobotFactory` : Usine pour robots industriels (WI-1)
- `RobotFactoryManager` : Gestionnaire central des usines

### 2. **Strategy Pattern**

**Localisation :** `src/Robot_factory/Strategies/`

**Objectif :** Valider les robots selon les règles spécifiques à leur catégorie.

**Classes principales :**

- `IValidationStrategy` : Interface de validation
- `MilitaryValidationStrategy` : Validation pour robots militaires
- `DomesticValidationStrategy` : Validation pour robots domestiques
- `IndustrialValidationStrategy` : Validation pour robots industriels
- `ValidationContext` : Gestionnaire des stratégies

**Règles de validation :**

- **Militaire (M)** : Pièces (M) ou (I), systèmes (M) ou (G)
- **Domestique (D)** : Pièces (D), (G) ou (I)
- **Industriel (I)** : Pièces (G) ou (I)

### 3. **Template Method Pattern**

**Localisation :** `src/Robot_factory/Service/TemplateManager.cs`

**Localisation :** `src/Robot_factory/Pattern/TemplateMethod/InstructionFileProcessor.cs`

**Objectif :** Standardiser le processus d'ajout de templates robots avec validation et celui des fichiers d'entrées et de sorties.

**Processus standardisé :**

1. **Validation des préconditions** (nom unique, pièces valides)
2. **Parsing des pièces** (conversion et vérification)
3. **Détermination de catégorie** (basée sur les pièces utilisées)
4. **Validation des contraintes** (via Strategy Pattern)
5. **Ajout au repository** (stockage et intégration)

**Classes principales :**

- `TemplateManager` : Implémentation du Template Method
- `CustomRobot` : Robots créés à partir de templates personnalisés
- `InstructionFileProcessor.cs` : Implémentation du template method
