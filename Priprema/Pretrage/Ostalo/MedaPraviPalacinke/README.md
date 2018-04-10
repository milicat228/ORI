<h1> TEKST </h1>

<p> Meda je vrlo gladan i odlučio je da želi da jede palačinke. Pre nego što napravi palačinke Meda mora da skupi sastojke. </p>

<i> Pravila: </i>
<ul>
  <li> Meda može da se kreće gore, dole, levo, desno, ali i po dijagonalama. </li>
  <li> Obavezna polja su bež boje i na njima će se nalaziti sličica sastojaka. (Ignorisati kako je dodana sličica, jer je nevažno) </li>
  <li> Ciljno polje je roze boje i na njemu se nalazi sličica palačinaka. (Ignorisati kako je dodana sličica, jer je nevažno) </li>
  <li> Pretraga je gotova tek kada Meda poseti sva polja na kojima su sastojci, a zatim palačinka polje </li>
  <li> Može da bude više polja sa sastojcima.  </li> 
</ul>

<i> Napomena: </i>
<ul>
  <li> Kada se hashcode koristi i računa kao trenutno mislim da može najviše 8 sastojaka ili tako nešto. </li> 
  <li> A* pretraga. Heuristika ostaje stara, Meda juri ka palačinkama i dok nema sve sastojke. </li>
</ul>

<h1> Ukratko </h1>
<ul>
  <li> U main-u se u delu gde se zapamte početno i krajnje stanje zapamti i lista obaveznih stanja. Pamte se kao Point - koordinate polja gde se nalazi obavezno polje. </li>
  <li> Klasa State sadrži Hashtable koji sadrži sva posećena polja. </li>
  <li> Kada se tumači da li je stanje krajnje, prođe se kroz listu obaveznih stanja i označe sva posećena. </li>
</ul>

