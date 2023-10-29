export function timeSince(date: Date) {
    const now = new Date();

    var seconds = Math.floor((now.getTime() - date.getTime()) / 1000);
    var interval = seconds / 31536000;
  
    if (interval > 1) {
        var result = " years"
        if(Math.floor(interval) == 1){
            result = " year"
        }
      return Math.floor(interval) + result + " ago";
    }
    interval = seconds / 2592000;
    if (interval > 1) {
        var result = " months"
        if(Math.floor(interval) == 1){
            result = " month"
        }
      return Math.floor(interval) + result + " ago";
    }
    interval = seconds / 86400;
    if (interval > 1) {
        var result = " days"
        if(Math.floor(interval) == 1){
            result = " day"
        }
      return Math.floor(interval) + result + " ago";
    }
    interval = seconds / 3600;
    if (interval > 1) {
        var result = " hours"
        if(Math.floor(interval) == 1){
            result = " hour"
        }
      return Math.floor(interval) + result + " ago";
    }
    interval = seconds / 60;
    if (interval > 1) {
        var result = " minutes"
        if(Math.floor(interval) == 1){
            result = " minute"
        }
      return Math.floor(interval) + result + " ago";
    }
    return "now";
  }