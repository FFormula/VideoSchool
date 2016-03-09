var notificationList = document.getElementsByClassName('notifications-list')[0]
ToggleNotifications();

function ToggleNotifications()
{
	if(notificationList.style.visibility == "hidden")
		notificationList.style.visibility = "visible"
	else
		notificationList.style.visibility = "hidden"
}