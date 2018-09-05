
// Client ID and API key from the Developer Console
var CLIENT_ID = '993332100456-9g7k5em7tf28gmtrs5i4q0rspgrrf9kg.apps.googleusercontent.com';

function authenticate() {
    return gapi.auth2.getAuthInstance()
        .signIn({ scope: "https://www.googleapis.com/auth/yt-analytics.readonly" })
        .then(function () { console.log("Sign-in successful"); },
        function (err) { console.error("Error signing in", err); });
}

function loadClient() {
    return gapi.client.load("https://youtubeanalytics.googleapis.com/$discovery/rest?version=v2")
        .then(function () { console.log("GAPI client loaded for API"); },
        function (err) { console.error("Error loading GAPI client for API", err); });
}
gapi.load("client:auth2", function () {
    gapi.auth2.init({ client_id: CLIENT_ID });
});


//----------------------------Basic stats----------------------------
// This example retrieves aggregated metrics for the channel's content. 
// The report returns a single row of data that contains totals for each requested metric during the specified date range.
function basicVideoStats() {
    return gapi.client.youtubeAnalytics.reports.query({
        "ids": "channel==MINE",
        "startDate": "2017-01-01",
        "endDate": "2017-12-31",
        "metrics": "views,comments,likes,dislikes,estimatedMinutesWatched,averageViewDuration",
        "maxResults": "100"
    })
        .then(function (response) {
            // Handle the results here (response.result has the parsed body).
            console.log(response);
        },
        function (err) { console.error("Execute error", err); });
}

//This example retrieves metrics for a specific country for a channel's videos. 
//The report returns a single row of data that contains totals for each requested metric during the specified date range.
function CountrySpecificViewCount() {
    return gapi.client.youtubeAnalytics.reports.query({
        "ids": "channel==MINE",
        "startDate": "2017-01-01",
        "endDate": "2017-12-31",
        "metrics": "views,comments,likes,dislikes,estimatedMinutesWatched",
        "filters": "country==VN"
    })
        .then(function (response) {
            // Handle the results here (response.result has the parsed body).
            console.log(response);
        },
        function (err) { console.error("Execute error", err); });
}

//This example retrieves metrics for a channel's 10 most watched videos, as measured by estimated minutes watched during the specified date range. 
//Results are sorted by estimated minutes watched in descending order.
function MostWatchVideo() {
    return gapi.client.youtubeAnalytics.reports.query({
        "ids": "channel==MINE",
        "startDate": "2017-01-01",
        "endDate": "2017-12-31",
        "metrics": "estimatedMinutesWatched,views,likes,subscribersGained",
        "dimensions": "video",
        "sort": "-estimatedMinutesWatched",
        "maxResults": "10"
    })
        .then(function (response) {
            // Handle the results here (response.result has the parsed body).
            console.log(response);
        },
        function (err) { console.error("Execute error", err); });
}


//----------------------------Time-based----------------------------
//This example retrieves daily view counts, watch time metrics, and new subscriber counts for a channel's videos. 
//The report returns one row of data for each day in the requested date range.Rows are sorted in chronological order.
function DailyVideoCount() {
    return gapi.client.youtubeAnalytics.reports.query({
        "ids": "channel==MINE",
        "startDate": "2017-01-01",
        "endDate": "2017-12-31",
        "metrics": "views,estimatedMinutesWatched,averageViewDuration,averageViewPercentage,subscribersGained",
        "dimensions": "day",
        "sort": "day"
    })
        .then(function (response) {
            // Handle the results here (response.result has the parsed body).
            console.log(response);
        },
        function (err) { console.error("Execute error", err); });
}

//This example retrieves monthly view counts, annotation click-through rates, 
//annotation close rates, and annotation impressions for the channel's content in the state of New York. Results are sorted in chronological order.
function MonthlyVideoCount() {
    return gapi.client.youtubeAnalytics.reports.query({
        "ids": "channel==MINE",
        "startDate": "2017-01-01",
        "endDate": "2018-05-01",
        "metrics": "views,annotationClickThroughRate,annotationCloseRate,annotationImpressions",
        "dimensions": "month",
        "sort": "month",
        "filters": "province==US-NY"
    })
        .then(function (response) {
            // Handle the results here (response.result has the parsed body).
            console.log(response);
        },
        function (err) { console.log(err); });
}

//----------------------------Traffic source----------------------------
//This example retrieves the number of views and estimated watch time for the channel's videos in a specified country. 
//The metrics are aggregated by traffic source, which describes the manner in which users reached the video.
function NumberOfViews() {
    return gapi.client.youtubeAnalytics.reports.query({
        "ids": "channel==MINE",
        "startDate": "2017-01-01",
        "endDate": "2018-05-01",
        "metrics": "views,estimatedMinutesWatched",
        "filters": "country==US"
    })
        .then(function (response) {
            // Handle the results here (response.result has the parsed body).
            console.log(response);
        },
        function (err) { console.log(err); });
}

//This example retrieves daily view counts and daily estimated watch time for the channel's videos. 
//The metrics are aggregated on a daily basis by traffic source and sorted in chronological order.
function DailyViewCount() {
    return gapi.client.youtubeAnalytics.reports.query({
        "ids": "channel==MINE",
        "startDate": "2017-01-01",
        "endDate": "2018-05-01",
        "metrics": "views,estimatedMinutesWatched",
        "dimensions": "day,insightTrafficSourceType",
        "sort": "day"
    })
        .then(function (response) {
            // Handle the results here (response.result has the parsed body).
            console.log(response);
        },
        function (err) { console.log(err); });
}

//This example retrieves the 10 search terms that generated the most views from YouTube search results for one or more specific videos. 
//Results are sorted by view count in descending order.Note that to run this query in the APIs Explorer, 
//you must replace the string VIDEO_ID in the filters parameter value with a comma- separated list of up to 200 video IDs.
function 10SearchTerm() {
    return gapi.client.youtubeAnalytics.reports.query({
        "ids": "channel==MINE",
        "startDate": "2017-01-01",
        "endDate": "2018-05-01",
        "metrics": "views",
        "dimensions": "insightTrafficSourceDetail",
        "sort": "-views",
        "filters": "video==VIDEO_ID;insightTrafficSourceType==YT_SEARCH",
        "maxResults": "10"
    })
        .then(function (response) {
            // Handle the results here (response.result has the parsed body).
            console.log(response);
        },
        function (err) { console.log(err); });
}

//----------------------------Basic stats----------------------------


//----------------------------Basic stats----------------------------

