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
[{"id":"S","tmi":90,"route":["JANJO","56.00,50.00","57.00,40.00","58.00,30.00","58.00,20.00","58.00,15.00","GOMUP","GINGA"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"T","tmi":90,"route":["LOMSI","55.00,50.00","56.00,40.00","57.00,30.00","57.00,20.00","SUNOT","KESIX"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"U","tmi":90,"route":["NEEKO","54.00,50.00","55.00,40.00","56.00,30.00","56.00,20.00","PIKIL","SOVED"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"V","tmi":90,"route":["RIKAL","53.00,50.00","54.00,40.00","55.00,30.00","55.00,20.00","RESNO","NETKI"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"W","tmi":90,"route":["TUDEP","52.00,50.00","53.00,40.00","54.00,30.00","54.00,20.00","DOGAL","BEXET"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"X","tmi":90,"route":["ALLRY","51.00,50.00","52.00,40.00","53.00,30.00","53.00,20.00","MALOT","GISTI"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"Y","tmi":90,"route":["ELSIR","50.00,50.00","51.00,40.00","52.00,30.00","52.00,20.00","LIMRI","XETBO"],"flightLevels":[320000,330000,340000,350000,360000,370000,380000,390000,400000],"direction":2},{"id":"Z","tmi":90,"route":["SOORY","43.00,50.00","46.00,40.00","49.00,30.00","50.00,20.00","SOMAX","ATSUR"],"flightLevels":[320000,340000,360000,380000,400000],"direction":2},{"id":"A","tmi":90,"route":["LIMRI","52.00,20.00","50.00,30.00","48.00,40.00","47.00,50.00","PORTI"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1},{"id":"B","tmi":90,"route":["DINIM","51.00,20.00","49.00,30.00","47.00,40.00","45.00,50.00","RAFIN"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1},{"id":"C","tmi":90,"route":["SOMAX","50.00,20.00","48.00,30.00","46.00,40.00","44.00,50.00","BOBTU","JAROM"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1},{"id":"D","tmi":90,"route":["BEDRA","49.00,20.00","47.00,30.00","45.00,40.00","43.00,50.00","42.00,60.00","DOVEY"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1},{"id":"E","tmi":90,"route":["ETIKI","48.00,15.00","48.00,20.00","46.00,30.00","44.00,40.00","42.00,50.00","41.00,60.00","JOBOC"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1}]
```

Example of response requesting a single track:
```
{"id":"A","tmi":90,"route":["LIMRI","52.00,20.00","50.00,30.00","48.00,40.00","47.00,50.00","PORTI"],"flightLevels":[310000,330000,340000,350000,370000,380000,390000],"direction":1}
```

Please do not distribute or upload this software to any internet site, server or otherwise without the express permission of myself.
