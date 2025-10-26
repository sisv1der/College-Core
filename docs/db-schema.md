```mermaid
---
title: College Attendance Control System
---
erDiagram
    USERS }o--o{ ROLES : "has_roles"
    ROLES }o--o{ PERMISSIONS : "has_permissions"
    USERS ||--|| PROFILES : "has_profile"
    PROFILES }o--o{ GROUPS : "belongs_to"
    PROFILES }o--o{ ABSENCES : "has_absences/recorded_by"
```