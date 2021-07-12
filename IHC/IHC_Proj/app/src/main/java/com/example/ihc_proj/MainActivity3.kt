package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton
import androidx.fragment.app.FragmentActivity
import com.google.android.gms.maps.*
import com.google.android.gms.maps.model.LatLng
import com.google.android.gms.maps.model.LatLngBounds
import com.google.android.gms.maps.model.MarkerOptions
import com.google.android.material.snackbar.Snackbar

class MainActivity3 : FragmentActivity(), OnMapReadyCallback  {

    var map : GoogleMap? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main3)

        var mapFragment : SupportMapFragment = supportFragmentManager.findFragmentById(R.id.map) as SupportMapFragment
        //findViewById<SupportMapFragment>(R.id.map)
        mapFragment.getMapAsync(this)
        //drone 1
        val button1 = findViewById<ImageButton>(R.id.drone1)
        button1.setOnClickListener{
            val intent1 = Intent(this, MainActivity4::class.java)
            startActivity(intent1)
        }

        //drone 2
        val button2 = findViewById<ImageButton>(R.id.drone2)
        button2.setOnClickListener{
            val intent2 = Intent(this, MainActivity11::class.java)
            startActivity(intent2)
        }

        //drone 3
        val button3 = findViewById<ImageButton>(R.id.drone3)
        button3.setOnClickListener{
            Snackbar.make(findViewById(R.id.myCoordinatorLayout_2), "Drone is already in use", Snackbar.LENGTH_SHORT).show()
        }

        //back button
        val buttonB = findViewById<ImageButton>(R.id.back_imageButton)
        buttonB.setOnClickListener{
            val intentB = Intent(this, MainActivity2::class.java)
            startActivity(intentB)
        }

        //chat button
        val buttonC = findViewById<ImageButton>(R.id.chat_button)
        buttonC.setOnClickListener{
            val intentC = Intent(this, MainActivity7::class.java)
            startActivity(intentC)
        }
    }

    override fun onMapReady(googleMap: GoogleMap) {
        googleMap.addMarker(
            MarkerOptions()
                .position(LatLng(0.0, 0.0))
                .title("Marker")
        )

        googleMap.moveCamera(CameraUpdateFactory.newLatLngZoom(LatLng(40.633636, -8.660394), 18f))

    }
}