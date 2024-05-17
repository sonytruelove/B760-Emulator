# B760-Emulator

Diagnosing mulfunctions with the B760 GAMING X motherboard. C# WPF

<h2>Review</h2>


![изображение](https://github.com/sonytruelove/B760-Emulator/assets/42536061/f6d82fb1-6a0f-4278-8244-acd84725af75)


The program contains 2 modes “Test” and “View”.
<p>
Mulfunctions:
<ul>
<li>"The CMOS battery is low"</li>
<li>"South Bridge faulty"</li>
<li>"USB voltage is not normal"</li>
<li>"SYSPWROK voltage is not normal"</li>
<li>"PLTRST voltage is not normal"</li>
<li>"Failure of the clock quartz resonator"</li>
<li>"BIOS failure"</li>
<li>"The main PWM controller of the processor is faulty"</li>
<li>"The RT7296F controller is faulty"</li>
<li>"AUX power phase PWM controller is faulty"</li>
<li>"Shattered SMDs"
  <ul>
<li>4 options</li></ul></li>
<li>"Damaged socket"</li>
<li>"Molex port is not working"</li>
<li>"The fan is not working"</li>
</ul>
<h3>"Test"</h3>
<p>
Motherboard model with random mulfunction.<br>
The user is prompted to perform diagnostics to identify this mulfunction.</p>
<h3>"View"</h3>
<p>
All motherboard faults presented in the program.<br>
The user is asked to look at the board to identify this fault.</p>

<h2>Active UI elements</h2>

Magnifying glass – visual inspection of the board<br>
Multimeter - voltage and resistance measurements<br>
Oscilloscope – oscillograms of elements<br>
Fan – connect the system fan to the board<br>



