package com.example.ihc_proj

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.ImageButton
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity


class MainActivity9 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main9)


        //photo button
        val imageButton1 = findViewById<ImageButton>(R.id.photo_imageButton_1)
        imageButton1.setOnClickListener{
            val intent1 = Intent(this, MainActivity5::class.java)
            startActivity(intent1)
        }

        //back button
        val button_b = findViewById<ImageButton>(R.id.back_imageButton_m)
        button_b.setOnClickListener{
            val intent_b = Intent(this, MainActivity11::class.java)
            startActivity(intent_b)
        }

        //info button
        val buttonI = findViewById<ImageButton>(R.id.imageButton_info)
        buttonI.setOnClickListener{
            val intentI= Intent(this, MainActivity15::class.java)
            startActivity(intentI)
        }

        //record button
        val intentP = Intent(this, MainActivity13::class.java)
        val imageButton2 = findViewById<ImageButton>(R.id.imageButton5_mr)

        imageButton2.setOnClickListener(object : View.OnClickListener {
            override fun onClick(v: View?) {
                val textV= findViewById<TextView>(R.id.textView2_mr)
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
    }
}