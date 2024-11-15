package com.example.smartpassuserdevice.data.model

import java.time.Instant
import java.util.UUID

data class GetAuthCardsRequest(
    val userId: UUID,
)