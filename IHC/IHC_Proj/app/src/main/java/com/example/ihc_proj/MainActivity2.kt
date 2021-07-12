package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton
import de.hdodenhof.circleimageview.CircleImageView

class MainActivity2 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main2)

        //drone button
        val button = findViewById<CircleImageView>(R.id.drone_imageButton)
        button.setOnClickListener{
            val intent = Intent(this, MainActivity3::class.java)
            startActivity(intent)
        }

        //back button
        val button1 = findViewById<ImageButton>(R.id.back_imageButton)
        button1.setOnClickListener{
            val intent1 = Intent(this, MainActivity::class.java)
            startActivity(intent1)
        }

        //participants chat
        val button2 = findViewById<CircleImageView>(R.id.participants_imageButton)
        button2.setOnClickListener {
            val intent2 = Intent(this, MainActivity7::class.java)
            startActivity(intent2)
        }

        //information button
        val buttonIB = findViewById<ImageButton>(R.id.imageButton2)
        buttonIB.setOnClickListener {
            val intentIB = Intent(this, MainActivity19::class.java) //antes tava MainActivity16
            startActivity(intentIB)
        }
    }
}