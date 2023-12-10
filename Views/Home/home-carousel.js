// home-carousel.js
function Carousel({ images }) {
    const [currentIndex, setCurrentIndex] = React.useState(0);
    const [startX, setStartX] = React.useState(0);
  
    const nextSlide = () => {
      const newIndex = (currentIndex + 1) % images.length;
      setCurrentIndex(newIndex);
    };
  
    const prevSlide = () => {
      setCurrentIndex((currentIndex - 1 + images.length) % images.length);
    };
  
    const handleMouseDown = (e) => {
      setStartX(e.pageX);
    };
  
    const handleMouseMove = (e) => {
      const walk = (e.pageX - startX) * 2; // Adjust the multiplier for sensitivity
      document.querySelector('.carousel').scrollLeft = document.querySelector('.carousel').scrollLeft - walk;
    };
  
    const handleTouchStart = (e) => {
      setStartX(e.touches[0].pageX);
    };
  
    const handleTouchMove = (e) => {
      const walk = (e.touches[0].pageX - startX) * 2; // Adjust the multiplier for sensitivity
      document.querySelector('.carousel').scrollLeft = document.querySelector('.carousel').scrollLeft - walk;
    };
  
    return (
      <div
        className="carousel-container"
        style={{ maxWidth: '100%', height: 'calc(100vh - 50px)', overflow: 'hidden' }}
        onMouseDown={handleMouseDown}
        onMouseMove={handleMouseMove}
        onTouchStart={handleTouchStart}
        onTouchMove={handleTouchMove}
      >
        <div className="carousel">
          {images.map((image, index) => (
            <div className="slide" key={index}>
              <img src={image} alt={`Slide ${index + 1}`} />
            </div>
          ))}
        </div>
        <button className="prev-button" onClick={prevSlide}>
          Previous
        </button>
        <button className="next-button" onClick={nextSlide}>
          Next
        </button>
      </div>
    );
  }
  
  // Attach the component to the global namespace
  window.Carousel = Carousel;
  