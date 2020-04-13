# NAT-Tracks
NAT Track API

Returns a JSON response of all current North Atlantic Tracks or a single North Atlantic Track.

### Format

1. NAT Identifier
2. TMI number
3. Route (as object with name, lat and long)
4. Available flight levels (feet or SI)
5. Direction (Enum value: 0 = Unknown, 1 = West, 2 = East)

### Usage
Get all tracks: `/api/tracks`

Get all tracks (altitude as metres): `/api/tracks?si=true`

Get single track: `/api/tracks?id={track ID}` (eg: `/api/tracks?id=a`)

Get single track (altitude as metres): `/api/tracks?id={track ID}&si=true`

### JSON Output
Example of response requesting all tracks (altitude in **feet**) (reduced data due to COVID19):
```
[{"id":"Z","tmi":"149","route":[{"name":"ALLRY","latitude":50.5,"longitude":-52.0},{"name":"51/50","latitude":51.0,"longitude":50.0},{"name":"53/40","latitude":53.0,"longitude":40.0},{"name":"55/30","latitude":55.0,"longitude":30.0},{"name":"55/20","latitude":55.0,"longitude":20.0},{"name":"DOGAL","latitude":54.0,"longitude":-15.0},{"name":"BEXET","latitude":54.0,"longitude":-14.0}],"flightLevels":[34000,36000,38000,40000],"direction":2},{"id":"A","tmi":"149","route":[{"name":"DOGAL","latitude":54.0,"longitude":-15.0},{"name":"55/20","latitude":55.0,"longitude":20.0},{"name":"57/30","latitude":57.0,"longitude":30.0},{"name":"58/40","latitude":58.0,"longitude":40.0},{"name":"58/50","latitude":58.0,"longitude":50.0},{"name":"CUDDY","latitude":56.7,"longitude":-57.0}],"flightLevels":[34000,35000,36000,37000,38000,39000],"direction":1}]
```

Example of response requesting a single track (altitude in **metres**):
```
{"id":"A","tmi":"149","route":[{"name":"DOGAL","latitude":54.0,"longitude":-15.0},{"name":"55/20","latitude":55.0,"longitude":20.0},{"name":"57/30","latitude":57.0,"longitude":30.0},{"name":"58/40","latitude":58.0,"longitude":40.0},{"name":"58/50","latitude":58.0,"longitude":50.0},{"name":"CUDDY","latitude":56.7,"longitude":-57.0}],"flightLevels":[10363,10668,10973,11278,11583,11887],"direction":1}
```
