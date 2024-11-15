package com.example.smartpassuserdevice.data.model

import java.time.Instant
import java.util.UUID

data class AuthCard (
    val id: UUID,

    val passKeys: String,

    val passIndex: Int,

    val userId: UUID,

    val cardType: CardType,

    val cardState: CardState,

    val accessLevelId: UUID,

    val description: String? = null,

    val lastUsingUtcDate: Instant? = null
)