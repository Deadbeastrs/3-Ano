package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.ImageButton
import android.widget.TextView

class MainActivity12 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main12)

        //photo button
        val imageButton1 = findViewById<ImageButton>(R.id.photo_imageButton_nn)
        imageButton1.setOnClickListener{
            val intent1 = Intent(this, MainActivity5::class.java)
            startActivity(intent1)
        }

        //back button
        val imageButton3 = findViewById<ImageButton>(R.id.back_imageButton_nn)
        imageButton3.setOnClickListener{
            val intent3 = Intent(this, MainActivity3::class.java)
            startActivity(intent3)
        }

        //record button
        val intentP = Intent(this, MainActivity5::class.java)
        val imageButton2 = findViewById<ImageButton>(R.id.imageButton5_nn)

        imageButton2.setOnClickListener(object : View.OnClickListener {
            override fun onClick(v: View?) {
                val textV= findViewById<TextView>(R.id.textView2_nn)
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
        val imageButton4 = findViewById<ImageButton>(R.id.manual_button_nn)
        imageButton4.setOnClickListener{
            val intent4 = Intent(this, MainActivity9::class.java)
            startActivity(intent4)
        }

        //chat button
        val buttonC = findViewById<ImageButton>(R.id.chat_button2_nn)
        buttonC.setOnClickListener{
            val intentC = Intent(this, MainActivity7::class.java)
            startActivity(intentC)
        }
    }
}