<h3>Contents</h3>
<li><a href="#about">About multitube.android</a></li>
<li><a href="#features">Key features</a></li>
<li><a href="#tech">Technologies used</a></li>
<li><a href="#gallery">App gallery</a></li>
<li><a href="#future">Future plans</a></li>
<li><a href="#download">App download</a></li>

<h6 align="center" id="about"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/App1/Resources/drawable/logo.png"><h6>
<h5 >Multitube android is a multi modules application that allows you to stream and download videos from youtube. This app have 4 main modules that are the following:<h5> 
<br><h6 align="center"><img color="black" src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/remote.png" height:"128" width:"128"/></h6>
<h3 align="center">Remote controller</h3> <br>
allows you to control the media of another device that has multitube installed and are currently playing media. like youtube tv does 
<br><h6 align="center"><img color="black" src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/youtube.png" height:"128" width:"128"/></h6>
<h3 align="center">Streaming player</h3> <br>
allows you to play videos from youtube natively like the youtube app does but this also allows you to play the media even if you have the phone blocked or have the app in background. this also has playlist feature, and autoplay and by the way alows you to download every media that is currently playing
<br><h6 align="center"><img color="black" src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/musicnote.png" height:"128" width:"128"/></h6>
<h3 align="center">Offline player</h3> <br>
once you have downloaded media you can play then in a custom player that have the sync feature that allow you to transfer your downloaded media to another device that has multitube installed and is currently 
in the same network of your device.
<br><h6 align="center"><img color="black" src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/videoinputantenna.png" height:"128" width:"128"/></h6>
<h3 align="center">Streaming server</h3> <br>
Once you have downloaded media you can stream that media from a webbrowser. without download anything in the other device. all you need to do is enable that feature in the setting menu and then it will show you an notification that show you your ip+the port number just enter that in the webbrowser of any device connected in your network to start streaming or you can just enter to <a href="multitubeweb.tk">Multitubeweb.tk</a>, and connect following the instructions detailed in the notification description
<br>
<br>
<h2 align="center" id="features">App KeyFeatures</h2>
<ul>
<li><b>You can play music/videos directly from youtube in background and even with the screen blocked</b></li>
<li>Inspired in material design </li>
<li>Fully native</li>
<li>Youtube audio/video streaming</li>
<li>Youtube audio/video download</li>
<li>Can be used as a remote controller or as server that can be controlled by others devices</li>
<li>Custom intent that alows you to download directly from youtube app using the share button</li>
<li>You can make your own web stream server based of the media that you have downloaded. <a href='https://github.com/Gr3gorywolf/Multitubeweb'>the web frontend is here</a></li>
<li>You can play the offline content that you previously downloaded from youtube with an built in custom media player</li>
<li>Built in custom youtube searcher</li>
<li>You can transfer media from one device with multitube to another</li>
<li>You can play/download media from the youtube's official app just using the share button and selecting "Open with multitube" (to play videos from youtube official app you must have the streaming player/remote controller running in background)</li>
<li>Autoplay function that plays the first suggested element just after the actual element finishes</li>
<li>You can load/create/modify/transfer playlist of media elements </li>
<li>Start menu that shows you the most played elements,last played elements, favourite videos, and suggestions</li>
<li>Custom searcher that shows you the exact results of youtube webpage search and allows you to download/play/enquenque easily</li>
<li>Start menu that shows you the most played elements,last played elements, favourite videos, and suggestions</li>  
</ul>
<br><h2 align="center" id="tech">Technologies/libraries used</h2>
   <h3>For the base application</h3>
 <ul>
<li>Xamarin.android</li> 
</ul>
    <h3>For the comunication over devices</h3>
 <ul>
<li>Native c# tcp listener/client as command transfer method</li> 
<li><a href="https://github.com/JamesNK/Newtonsoft.Json">Newtonsoft.JSON</a> for json encryption/decryption</li>
<li>Custom http server based on native c# tcp listener/client</li>
</ul>
 <h3>For UI components</h3>
 <ul>
<li>Native android appcompat widgets</li> 
<li><a href="https://github.com/Cheesebaron/SlidingUpPanel">Custom slideup panel for the players</a></li>
</ul>   
<h3>For the youtube info extraction</h3>
 <ul>
<li><a href="https://github.com/omansak/libvideo">Omansak's libvideo</a></li> 
<li><a href="https://github.com/mrklintscher/YoutubeSearch">Youtube search by mrklintscher</a></li> 
<li><a href="https://github.com/zzzprojects/html-agility-pack">Html agility pack</a> for some manual scraps</li> 
</ul>  
<h3>For the QR code scanning/reading</h3>
 <ul>
<li><a href="https://github.com/Redth/ZXing.Net.Mobile">Zxing (is the best actually xD)</a></li> 
</ul>  
<h3>For Image catching/optimization</h3>
 <ul>
<li><a href="https://github.com/jonathanpeppers/glidex">Glidex (easy,fast,portable. the best of the world)</a></li> 
</ul> 
<h3>Firebase integration</h3>
 <ul>
<li><a href="https://github.com/rlamasb/Firebase.Xamarin">Firebase.Xamarin</a></li> 
</ul> 
<br><h2 align="center" id="gallery">Gallery</h2>  
<h5 align="center"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/screenshots/1.jpeg" width="35%" ></img></h5>
<h5 align="center"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/screenshots/2.jpeg" width="35%" ></img></h5>
<h5 align="center"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/screenshots/3.jpeg" width="35%" ></img></h5> 
<h5 align="center"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/screenshots/4.jpg" width="35%" ></img></h5>
<h5 align="center"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/screenshots/42.jpeg" width="35%" ></img></h5>
<h5 align="center"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/screenshots/5.jpeg" width="35%" ></img> </h5>
<h5 align="center"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/screenshots/6.jpeg" width="35%" ></img>  </h5>   
<h5 align="center"><img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/screenshots/7.jpeg" width="35%" ></img>  </h5>
<br><h2 align="center" id="future">Future plans (ToDo)</h2>   
   <ul>
       <li><b>(Coming soon)</b>App userguide </li>
      <li>Youtube download from webbrowser using device as <b>Remote scraper</b></li>
       <li>Offline player playlists</li>
       <li>Finish the develop of the <a href="https://github.com/Gr3gorywolf/Multitube-desktop">desktop application</a></li>
       <li>Redesign the current webpage (actually was created in 2015 and i had no idea about web development)</li>
       <li>Cloud server that will allow you to use it from the multitube webpage (provably i will use nodejs as backend)</li>     
   </ul>
   
<br><h2 align="center" id="download">App Direct download</h2>   
  <h3 align="center"> <a href='https://gr3gorywolf.github.io/getromdownload/youtubepc.html' > <img src="https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/docs/img/download.png" /></a></h3>

