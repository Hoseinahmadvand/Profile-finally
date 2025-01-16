

let currentImageIndex = 0;
const images = document.querySelectorAll('.gallery-img');

// Open modal and show the selected image
function openModal(index) {
    currentImageIndex = index;
    const imageSrc = images[currentImageIndex].src;
    document.getElementById('modalImage').src = imageSrc;
    const galleryModal = new bootstrap.Modal(document.getElementById('galleryModal'));
    galleryModal.show();
}

// Change image on "Next" or "Previous" button click
function changeImage(direction) {
    currentImageIndex += direction;

    // Loop back to the start/end if at the boundary
    if (currentImageIndex < 0) {
        currentImageIndex = images.length - 1;
    } else if (currentImageIndex >= images.length) {
        currentImageIndex = 0;
    }

    const imageSrc = images[currentImageIndex].src;
    document.getElementById('modalImage').src = imageSrc;
}


// Translation data
const translations = {
    en: {
        home: "Home",
        projects: "Projects",
        skills: "Skills",
        gallery: "Gallery",
        about: "About",
        contact: "Contact"
    },
    fa: {
        home: "خانه",
        projects: "پروژه‌ها",
        skills: "مهارت‌ها",
        gallery: "گالری",
        about: "درباره من",
        contact: "تماس"
    }
};

// Function to change language
function changeLanguage(language) {
    const elements = document.querySelectorAll("[data-lang]");
    elements.forEach((element) => {
        const key = element.getAttribute("data-lang");
        element.innerText = translations[language][key];
    });
}

// Set default language on page load
document.addEventListener("DOMContentLoaded", () => {
    changeLanguage("en"); // Set default language to English
});


const carousel = document.getElementById('carouselExampleControls');
carousel.addEventListener('slide.bs.carousel', (event) => {
    const videos = document.querySelectorAll('.fullscreen-video');
    videos.forEach(video => video.pause());

    //const activeVideo = event.relatedTarget.querySelector('.fullscreen-video');
    //if (activeVideo) {
    //    activeVideo.play();
    //}
});







