package com.example.smartpassuserdevice.ui.apiFunctionality

import com.example.smartpassuserdevice.data.model.CardState
import com.example.smartpassuserdevice.data.model.CardType
import com.squareup.moshi.FromJson
import com.squareup.moshi.Moshi
import com.squareup.moshi.ToJson
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.moshi.MoshiConverterFactory
import java.util.concurrent.TimeUnit
import java.util.UUID
import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale


class DateJsonAdapter {
    private val dateFormat = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.US)

    @ToJson
    fun toJson(date: Date): String {
        return dateFormat.format(date)
    }

    @FromJson
    fun fromJson(dateString: String): Date {
        return dateFormat.parse(dateString)!!
    }
}

class UUIDJsonAdapter {
    @ToJson
    fun toJson(uuid: UUID): String {
        return uuid.toString()
    }

    @FromJson
    fun fromJson(uuidString: String): UUID {
        return UUID.fromString(uuidString)
    }
}

class CardTypeAdapter {
    @FromJson
    fun fromJson(code: Int): CardType {
        return CardType.fromCode(code)
    }

    @ToJson
    fun toJson(cardType: CardType): Int {
        return cardType.code
    }
}

class CardStateAdapter {
    @FromJson
    fun fromJson(code: Int): CardState {
        return CardState.fromCode(code)
    }

    @ToJson
    fun toJson(cardState: CardState): Int {
        return cardState.code
    }
}

class ApiServiceFactory {

    private val BASE_URL = "http://10.0.2.2:5295/api/MobileUser/"

    // Адаптер для Date
    private val moshi: Moshi by lazy {
        Moshi.Builder()
            .add(DateJsonAdapter())
            .add(UUIDJsonAdapter())
            .add(CardTypeAdapter())
            .add(CardStateAdapter())
            .build()
    }

    private val client: OkHttpClient by lazy {
        val logging = HttpLoggingInterceptor().apply {
            level = HttpLoggingInterceptor.Level.BODY
        }
        OkHttpClient.Builder()
            .addInterceptor(logging)
            .hostnameVerifier { _, _ -> true }  // Игнорирование проверки имени хоста
            .connectTimeout(30, TimeUnit.SECONDS)
            .readTimeout(30, TimeUnit.SECONDS)
            .writeTimeout(30, TimeUnit.SECONDS)
            .build()
    }

    private val retrofit: Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .client(client)
            .addConverterFactory(MoshiConverterFactory.create(moshi)) // Передаем Moshi с адаптером
            .build()
    }

    fun getApiService(): ApiService {
        return retrofit.create(ApiService::class.java)
    }
}