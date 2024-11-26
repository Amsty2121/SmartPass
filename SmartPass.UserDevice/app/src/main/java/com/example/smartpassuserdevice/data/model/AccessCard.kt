package com.example.smartpassuserdevice.data.model

import java.util.Date
import java.util.UUID

data class AccessCard (
    val id: UUID,
    val passKeys: String,
    val passIndex: Int,
    val userId: UUID?,
    val userName: String,
    val cardType: CardType,
    val cardState: CardState,
    val accessLevelId: UUID,
    val accessLevelName: String,
    val description: String?,
    val lastUsingUtcDate: Date?
)