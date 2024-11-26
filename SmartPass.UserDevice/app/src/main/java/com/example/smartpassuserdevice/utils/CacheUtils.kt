import android.content.Context
import android.content.SharedPreferences
import com.example.smartpassuserdevice.data.model.LoginResponse
import com.google.gson.Gson
import java.util.*
import com.auth0.android.jwt.JWT
import com.example.smartpassuserdevice.data.model.GetMyAccessCardsMobileResponse
import com.example.smartpassuserdevice.data.model.UserInfo

object CacheUtils {

    private const val PREF_NAME = "auth_data"
    private const val KEY_TOKEN = "token"
    private const val KEY_REFRESH_TOKEN = "refresh_token"
    private const val KEY_AUTH_KEY = "auth_key"
    private const val KEY_USER_INFO = "user_info"
    private const val KEY_USER_CARDS = "user_cards"

    private fun getSharedPreferences(context: Context): SharedPreferences {
        return context.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE)
    }

    fun saveLoginResponseToCache(context: Context, loginResponse: LoginResponse) {
        val sharedPreferences = getSharedPreferences(context)

        // Декодируем JWT и получаем информацию о пользователе
        val jwtToken = JWT(loginResponse.token)
        val refreshToken = loginResponse.refreshToken

        /*val signInKeys = jwtRefreshToken.getClaim("SignInKeys").asString() ?: ""
        val signInIndex = jwtRefreshToken.getClaim("SignInIndex").asInt() ?: 1
        // Создание объекта AuthKey
        val authKey = AuthKey(signInKeys = signInKeys, signInIndex = signInIndex)*/

        val idString = jwtToken.getClaim("Id").asString()
        val userName = jwtToken.getClaim("UserName").asString() ?: ""
        val department = jwtToken.getClaim("Department").asString() ?: ""
        val id = idString?.let { UUID.fromString(it) } ?: UUID.randomUUID()

        // Создание объекта UserInfo
        val userInfo = UserInfo(id = id, userName = userName, department = department)

        // Сохраняем данные в SharedPreferences
        with(sharedPreferences.edit()) {
            putString(KEY_TOKEN, loginResponse.token)
            putString(KEY_REFRESH_TOKEN, refreshToken.toString())
            //putString(KEY_AUTH_KEY, Gson().toJson(authKey))
            putString(KEY_USER_INFO, Gson().toJson(userInfo))
            apply()
        }
    }

    fun saveUserCardsResponseToCache(context: Context, myCardsResponse: GetMyAccessCardsMobileResponse) {
        val sharedPreferences = getSharedPreferences(context)

        // Сохраняем данные в SharedPreferences
        with(sharedPreferences.edit()) {
            putString(KEY_USER_CARDS, Gson().toJson(myCardsResponse))
            apply()
        }
    }

    // Получаем информацию о AuthKey из SharedPreferences
    /*fun getAuthKeyFromCache(context: Context): AuthKey? {
        val sharedPreferences = getSharedPreferences(context)
        val authKeyJson = sharedPreferences.getString(KEY_AUTH_KEY, null)
        return if (authKeyJson != null) {
            Gson().fromJson(authKeyJson, AuthKey::class.java)
        } else {
            null
        }
    }*/

    // Получаем информацию о пользователе из SharedPreferences
    fun getUserInfoFromCache(context: Context): UserInfo? {
        val sharedPreferences = getSharedPreferences(context)
        val userInfoJson = sharedPreferences.getString(KEY_USER_INFO, null)
        return if (userInfoJson != null) {
            Gson().fromJson(userInfoJson, UserInfo::class.java)
        } else {
            null
        }
    }

    // Получаем токен из SharedPreferences
    fun getUserCardsFromCache(context: Context): GetMyAccessCardsMobileResponse? {
        val sharedPreferences = getSharedPreferences(context)
        val userCardsJson = sharedPreferences.getString(KEY_USER_CARDS, null)
        return if (userCardsJson != null) {
            Gson().fromJson(userCardsJson, GetMyAccessCardsMobileResponse::class.java)
        } else {
            null
        }
    }

    // Получаем токен из SharedPreferences
    fun getTokenFromCache(context: Context): String? {
        val sharedPreferences = getSharedPreferences(context)
        return sharedPreferences.getString(KEY_TOKEN, null)
    }

    // Получаем refreshToken из SharedPreferences
    fun getRefreshTokenDataFromCache(context: Context): String? {
        val sharedPreferences = getSharedPreferences(context)
        return sharedPreferences.getString(KEY_REFRESH_TOKEN, null)
    }

    // Удаляем данные из SharedPreferences
    fun clearCache(context: Context) {
        val sharedPreferences = getSharedPreferences(context)
        with(sharedPreferences.edit()) {
            if (sharedPreferences.contains(KEY_TOKEN)) {
                remove(KEY_TOKEN)
            }
            if (sharedPreferences.contains(KEY_REFRESH_TOKEN)) {
                remove(KEY_REFRESH_TOKEN)
            }
            if (sharedPreferences.contains(KEY_USER_INFO)) {
                remove(KEY_USER_INFO)
            }
            // Можно добавить аналогичную проверку для других ключей, если нужно
            // if (sharedPreferences.contains(KEY_AUTH_KEY)) {
            //     remove(KEY_AUTH_KEY)
            // }
            apply()
        }
    }

}