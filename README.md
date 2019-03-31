# NAT-Tracks
NAT Track API

Returns a JSON response of all current North Atlantic Tracks or a single North Atlantic Track in the format of:

1. NAT Identifier
2. TMI number
3. Route (includes parseable decimal coordinates)
4. Available flight levels
5. Direction (Enum value: 0 = Unknown, 1 = West, 2 = East)

Example of response requesting all tracks:
```
[{"id":"S","tmi":90,"route":["JANJO","56N50W","57N40W","58N30W","58N20W","58N15W","GOMUP","GINGA"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"T","tmi":90,"route":["LOMSI","55N50W","56N40W","57N30W","57N20W","SUNOT","KESIX"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"U","tmi":90,"route":["NEEKO","54N50W","55N40W","56N30W","56N20W","PIKIL","SOVED"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"V","tmi":90,"route":["RIKAL","53N50W","54N40W","55N30W","55N20W","RESNO","NETKI"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"W","tmi":90,"route":["TUDEP","52N50W","53N40W","54N30W","54N20W","DOGAL","BEXET"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"X","tmi":90,"route":["ALLRY","51N50W","52N40W","53N30W","53N20W","MALOT","GISTI"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"Y","tmi":90,"route":["ELSIR","50N50W","51N40W","52N30W","52N20W","LIMRI","XETBO"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"Z","tmi":90,"route":["SOORY","43N50W","46N40W","49N30W","50N20W","SOMAX","ATSUR"],"flightLevels":[320000,340000,360000,380000,400000],"direction":2},{"id":"A","tmi":90,"route":["LIMRI","52N20W","50N30W","48N40W","47N50W","PORTI"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1},{"id":"B","tmi":90,"route":["DINIM","51N20W","49N30W","47N40W","45N50W","RAFIN"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1},{"id":"C","tmi":90,"route":["SOMAX","50N20W","48N30W","46N40W","44N50W","BOBTU","JAROM"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1},{"id":"D","tmi":90,"route":["BEDRA","49N20W","47N30W","45N40W","43N50W","42N60W","DOVEY"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1},{"id":"E","tmi":90,"route":["ETIKI","48N15W","48N20W","46N30W","44N40W","42N50W","41N60W","JOBOC"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1}]
```

Example of response requesting a single track:
```
{"id":"A","tmi":90,"route":["LIMRI","52N20W","50N30W","48N40W","47N50W","PORTI"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1}
```

Please do not distribute or upload this software to any internet site, server or otherwise without the express permission of myself.
