package com.example.ihc_proj

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.ImageButton
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.snackbar.Snackbar

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        //sphericalView = findViewById<SphericalView>(R.id.spherical_view);
        //sphericalView.setPanorama(PLUtils.getBitmap(this, R.drawable.sunset_at_pier), true);
        /*
        val intent2 = Intent(this, MainActivity8::class.java)
        val editTextM = findViewById<EditText>(R.id.editTextTextPersonName4)
        intent2.putExtra("message_key", editTextM.getText().toString())
         */

        val button_first = findViewById<Button>(R.id.next_button1)
        button_first.setOnClickListener{
            val editText1 = findViewById<EditText>(R.id.editTextTextPersonName5)
            if(editText1.getText().toString().equals("id1234")) {
                val intent = Intent(this, MainActivity2::class.java)
                startActivity(intent)
            }else{
                Snackbar.make(findViewById(R.id.myCoordinatorLayout), "Invalid Mission ID", Snackbar.LENGTH_SHORT).show()
            }
        }
    }

    override fun onResume() {
        super.onResume()
    }

    override fun onPause() {
        super.onPause()
    }

    override fun onDestroy() {
        super.onDestroy()
    }
}