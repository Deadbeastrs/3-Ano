package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.ImageButton
import android.widget.TextView
import androidx.appcompat.widget.AppCompatButton
import androidx.core.content.ContextCompat
import com.google.android.material.snackbar.Snackbar

class MainActivity4 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main4)

        //photo button
        val imageButton1 = findViewById<ImageButton>(R.id.photo_imageButton)
        imageButton1.setOnClickListener{
            val intent1 = Intent(this, MainActivity5::class.java)
            startActivity(intent1)
        }

        //back button
        val imageButton3 = findViewById<ImageButton>(R.id.back_imageButton)
        imageButton3.setOnClickListener{
            val intent3 = Intent(this, MainActivity3::class.java)
            startActivity(intent3)
        }

        //record button
        val intentP = Intent(this, MainActivity13::class.java)
        val imageButton2 = findViewById<ImageButton>(R.id.imageButton5)

        imageButton2.setOnClickListener(object : View.OnClickListener {
            override fun onClick(v: View?) {
                val textV= findViewById<TextView>(R.id.textView2)
                if(imageButton2.getDrawable().getConstantState() == getResources().getDrawable( R.drawable.stop_record_1).getConstantState()){
                    imageButton2.setImageResource(R.drawable.start_record_1)
                    textV.text="Record"
                    startActivity(intentP)
                }else{
                    imageButton2.setImageResource(R.drawable.stop_record_1)
                    textV.text="Recording..."
                }
            }
        })

        //manual button
        val imageButton4 = findViewById<ImageButton>(R.id.manual_button)
        imageButton4.setOnClickListener{
            val intent4 = Intent(this, MainActivity9::class.java)
            startActivity(intent4)
        }

        //chat button
        val buttonC = findViewById<ImageButton>(R.id.chat_button2)
        buttonC.setOnClickListener{
            val intentC = Intent(this, MainActivity7::class.java)
            startActivity(intentC)
        }

        //emergency button
        val buttonEmg = findViewById<AppCompatButton>(R.id.emergency_button)
        buttonEmg.setOnClickListener{
            Snackbar.make(findViewById(R.id.myCoordinatorLayout_drone1), "Drone returning to base", Snackbar.LENGTH_SHORT).show()
        }
    }
}