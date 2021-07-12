package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageButton

class MainActivity16 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main16)

        //back button
        val button_b = findViewById<ImageButton>(R.id.back_imageButton_def)
        button_b.setOnClickListener{
            val intent_b = Intent(this, MainActivity2::class.java)
            startActivity(intent_b)
        }
    }
}
