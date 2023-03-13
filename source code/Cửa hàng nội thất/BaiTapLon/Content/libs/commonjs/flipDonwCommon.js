document.addEventListener('DOMContentLoaded', () => {

    // Unix timestamp (in seconds) to count down to
    var twoDaysFromNow = (new Date().getTime() / 1000) + (86400 * 7) + 1;

    // Set up FlipDown
    var flipdown = new FlipDown(twoDaysFromNow, { headings: ["Ngày", "Giờ", "Phút", "Giây"] })

      // Start the countdown
      .start()

      // Do something when the countdown ends
      .ifEnded(() => {
          console.log('Ưu đãi kết thúc!');
      });

    // Toggle theme
    var interval = setInterval(() => {
        let body = document.body;
        body.classList.toggle('light-theme');
        body.querySelector('#flipdown').classList.toggle('flipdown__theme-dark');
        body.querySelector('#flipdown').classList.toggle('flipdown__theme-dark');
    }, 5000);
});